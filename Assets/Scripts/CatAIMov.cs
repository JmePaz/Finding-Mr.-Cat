using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAIMov : MonoBehaviour
{
    [SerializeField] float field_view_rad;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] GameObject body;

    [SerializeField] GameObject avoidObject;
    [SerializeField] LayerMask avoidObjectLayerMask;

     [SerializeField] float reactionTime;
    CatProjectile catProjectile;
    Transform target;

    public GameObject currLand;

    // Start is called before the first frame update
    void Start()
    {
        catProjectile = GetComponent<CatProjectile>();
        Debug.Log("Running");
        StartCoroutine(InitiateAIMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //initate a Jump Sequence
    IEnumerator InitiateAIMovement(){
        do{
            yield return new WaitForSeconds(reactionTime);
            target = SelectTarget();
        
        }while(target == null);
        Debug.Log("Detected: "+target.gameObject);

       StartCoroutine(catProjectile.SmartJumpSequence(target, currLand,false));
    }


    // detect a new land target
    Transform SelectTarget(){
        Collider[] detectedColliders = Physics.OverlapSphere(this.transform.position, field_view_rad, targetLayerMask);
        if(detectedColliders.Length>0){
            bool isToAvoid = avoidObject!=null&&Physics.OverlapSphere(this.transform.position, field_view_rad, avoidObjectLayerMask).Length > 0;
            return GetPlatform(detectedColliders, isToAvoid);
        }
        return null;
    }

    // this filtered platforms
    private Transform GetPlatform(Collider[] detectedColliders, bool isToAvoid){
         List<Collider> filteredColliders = new List<Collider>();
        float maxAvoidanceScore = float.MinValue;
        Transform platform = null;
        foreach(var col in detectedColliders){
                if(col.gameObject != currLand){
                    if(isToAvoid){
                        float curr_avoidanceScore = (col.gameObject.transform.position - avoidObject.transform.position).magnitude;
                        if(curr_avoidanceScore>maxAvoidanceScore){
                            maxAvoidanceScore = curr_avoidanceScore;
                            platform = col.gameObject.transform;
                        }
                    }
                    else{
                        filteredColliders.Add(col);
                    }
                }
            }

        if(!isToAvoid){
                platform = filteredColliders[Random.Range(0, filteredColliders.Count)].gameObject.transform;
         }
         return platform;
    }

    private void OnCollisionEnter(Collision other) {

       if(other.gameObject.tag == "Obstacles"){
            //mark the land and start jumping to it
            if(currLand != other.gameObject){
                currLand = other.gameObject;
                //set as a child
                //this.transform.SetParent(currLand.transform);
                // iniate a Ai Movement
                StopAllCoroutines();
                StartCoroutine(InitiateAIMovement());
            }

       }
    }

    private bool IsJumpEarly(Transform currPlatform, Transform newPlatform){
        return newPlatform.position.y > currPlatform.position.y && CheckInBounds(currPlatform, newPlatform);
    }

    // checkbounds
    private bool CheckInBounds(Transform t1, Transform t2){
        if(t1.position.x-t1.localScale.x <= t2.position.x && t2.position.x <= t1.position.x + t1.localScale.x){
            return t1.position.z-t1.localScale.z <= t2.position.z && t2.position.z <= t1.position.z + t1.localScale.z;
        }
        return false;
    }

    //debug vision 
    void OnDrawGizmos(){
         Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, field_view_rad);
        //  Vector3 origin = this.transform.position + (this.transform.forward * body.transform.lossyScale.x*2f)+ (Vector3.down*body.transform.lossyScale.y);
        //  Gizmos.DrawLine(origin, origin+Vector3.down*body.transform.lossyScale.y*1.5f);
    }
}
