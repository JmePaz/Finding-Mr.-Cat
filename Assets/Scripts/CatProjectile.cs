using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatProjectile : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float time;
    [SerializeField] LayerMask obstacleLayerMask;

    [SerializeField] GameObject body;
    int obstacleLayerNum;
    CatAnimator catAnimController;
    Rigidbody myRigidBody;
    float gravity_y, max_height;
    // Start is called before the first frame update

    GameObject currLand;

    void Start()
    {
        max_height = float.MaxValue;
        gravity_y = Physics.gravity.y;
        myRigidBody = GetComponent<Rigidbody>();
        catAnimController = GetComponentInChildren<CatAnimator>();
        
        myRigidBody.freezeRotation = true;
        obstacleLayerNum  =(int) Mathf.Log(obstacleLayerMask.value,2);

    }

    // Update is called once per frame
    public IEnumerator SmartJumpSequence(Transform targetTransform, GameObject currLand, bool isEarlyJump=false){
        this.currLand = currLand;
        LookAtTarget(targetTransform);
        
        while(!catAnimController.IsOnJumpSequence){
            Vector3 origin = this.transform.position + (Vector3.down*body.transform.lossyScale.y);
            bool raycastDown = ImplementRayCast(origin, Vector3.down, body.transform.lossyScale.y*1.5f, obstacleLayerMask);
            if(  isEarlyJump||!raycastDown){
                //set as parent as null
                this.transform.SetParent(null);
                //intiate projectile movement
                // - > jump air
                SmartJump(targetTransform);
                // - > jump down
                StartCoroutine(JumpDownSequence());
            }
            else{
                Run();
            }
            yield return null;    
        }
        
    }
      void SmartJump(Transform targetTransform){
        //initalize calculations
        Debug.Log("Detected target: " + targetTransform.position);
        Vector3 offSettargetPos =  targetTransform.position + Vector3.up * (targetTransform.localScale.y/2);
        // float targetSpeed = targetTransform.gameObject.GetComponent<ObstacleMovement>().speed;
        // offSettargetPos += Vector3.down*targetSpeed*time;


        //calculate in law of Physics
        Vector3 initVelocity = CalcInitVelocity(offSettargetPos, this.transform.position, time);
        max_height = CalcMaxHeight(initVelocity);

        //integrated with game object
        catAnimController.ResetAllTriggers();
        catAnimController.JumpOnAirAnim();
        Debug.Log("Working: "+initVelocity);
        Physics.IgnoreLayerCollision(obstacleLayerNum, this.gameObject.layer, true);
        myRigidBody.velocity = initVelocity;
        //ignore for safety jumping while not interrupting
        
   }

    // jump down sequence of Cat
    IEnumerator JumpDownSequence(){
        float curr_height = float.MinValue;
        while(catAnimController.IsOnJumpSequence && this.transform.position.y>=curr_height){
            curr_height  = this.transform.position.y;
            yield return null;
        }
        catAnimController.JumpDownAnim();
       //myRigidBody.AddForce(Vector3.down*300,ForceMode.VelocityChange);
       
       Physics.IgnoreLayerCollision(obstacleLayerNum, this.gameObject.layer, false);
    }
 
  

    // rotate to the target
   void LookAtTarget(Transform targetTransform){
        ToggleRotations();
        Vector3 targetPos = targetTransform.position;
        targetPos.y = this.transform.position.y;
        this.transform.LookAt(targetPos);
        ToggleRotations();
   }

 

   //raycast
   public bool ImplementRayCast(Vector3 origin, Vector3 direction,float length, LayerMask targetLayerMask){
        Debug.DrawRay(origin, direction*length, Color.green, Time.deltaTime);
        return Physics.Raycast(origin, Vector3.down, length,targetLayerMask);
   }

    void Run(){
        catAnimController.RunAnim();
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    Vector3 CalcInitVelocity(Vector3 target, Vector3 origin, float time){
        Vector3 dist = (target - origin);
        Vector3 distXZ = dist;
        distXZ.y = 0;

        float m_XZ = distXZ.magnitude;
        float m_y = dist.y;

        float v_xz = m_XZ/time;
        float v_y = m_y/time + 0.5f * -gravity_y * time;

        Vector3 direction = distXZ.normalized;
        Vector3 initVelocity = direction * v_xz;
        initVelocity.y = v_y;

        return initVelocity;
    }

    float CalcMaxHeight(Vector3 initVelocity){

        return Mathf.Floor(Mathf.Pow(initVelocity.y, 2)/(2*Mathf.Abs(gravity_y)));

    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Obstacles" && catAnimController.IsOnJumpSequence){
            Debug.Log("Has Landed");
            catAnimController.LandedAnim();
            catAnimController.IdleAnim();
        }
    }   

    private void ToggleRotations(){
        myRigidBody.freezeRotation = !myRigidBody.freezeRotation;
    }

 

}
