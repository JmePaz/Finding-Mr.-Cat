using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAIMovement: MonoBehaviour{

    CatAnimator catAnimController;
    Rigidbody myRigidBody;

    [SerializeField] float speed;

    [SerializeField]GameObject yarn;
    bool isOnJumpSequence;
    bool isJumpOnAir;

    //on jump sequence

    float angle;
    [SerializeField][Range(0.0f, 1.0f)] float hypotenuse;
    float grav;

    float offSetYPos;
    float time;


    void Start()
    {
        catAnimController = GetComponentInChildren<CatAnimator>();
        myRigidBody = GetComponent<Rigidbody>();
        isOnJumpSequence = false;
        isJumpOnAir = false;

        angle = 0;
        grav = Physics.gravity.y;
        offSetYPos = this.transform.position.y;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0)&&!isOnJumpSequence){
             Vector3 targetDir = (yarn.transform.position-this.transform.position );
             Vector3 lastPos = this.transform.position;
             this.transform.LookAt(yarn.transform.position);
             //hypotenuse = CalculateHypotenus(yarn.transform.position/2);
            //  Quaternion q0 = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), 9*Time.deltaTime);
            //  myRigidBody.MoveRotation(q0);
            //  transform.position = lastPos;
            //this.transform.Rotate(-Vector3.up*10f*Time.deltaTime);

             StopAllCoroutines();
            StartCoroutine(SmartJump(this.transform.forward));
        }
     

    }

    float CalculateHypotenus(Vector3 targetPos){
        float adj = Mathf.Abs(targetPos.x - this.transform.position.x)/2;
        return (adj/Mathf.Cos(90))/180f;
    }

    IEnumerator SmartJump(Vector3 targetDir){

        while(angle<=180.0f){
            
            if(!isOnJumpSequence){
                catAnimController.JumpOnAirAnim();
                isOnJumpSequence = true;
                isJumpOnAir = true;
            }
            else if(isJumpOnAir && angle>=91.0f){
                catAnimController.JumpDownAnim();
                isJumpOnAir = false;
            }

            Vector3 newPos = CalculateTajectoryOnPos(targetDir, hypotenuse);

            this.transform.position = newPos;
            angle++;
            yield return null;
        }
    }

    Vector3 CalculateTajectoryOnPos(Vector3 direction, float hypotenuse){
        time += Time.deltaTime;
        float radian = Mathf.Deg2Rad * angle;
        float x =  Mathf.Cos(90*Mathf.Deg2Rad-radian)*time*hypotenuse;
        float y = Mathf.Sin(radian)*time*hypotenuse*-grav;

        Vector3 newPos = this.transform.position + direction*x ;
        newPos.y = offSetYPos+ y;
        return newPos;
    }

    void OldControls(){
        if(Input.GetKey(KeyCode.R)){
            catAnimController.RunAnim();
            Move(Vector3.left*10);
        }
        else if(Input.GetKeyDown(KeyCode.I)){
            catAnimController.IdleAnim();
        }
        else if(Input.GetKey(KeyCode.J)){
            if(!isOnJumpSequence){
                catAnimController.JumpOnAirAnim();
                isOnJumpSequence = true;
                isJumpOnAir = true;
            }
            myRigidBody.AddRelativeForce(Vector3.up*1000*Time.deltaTime);
        }
        else if(Input.GetKeyUp(KeyCode.J)){
            Debug.Log("Jumping down");
            catAnimController.JumpDownAnim();
            isJumpOnAir = false;
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x,-myRigidBody.velocity.y,myRigidBody.velocity.z);
        }
    }

    void Move(Vector3 direction){
        this.transform.position += direction*speed*Time.deltaTime;
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("Colided: "+other.gameObject.name);
        if(isOnJumpSequence&&!isJumpOnAir && other.gameObject.tag == "Obstacles"){
            Debug.Log("Has landed");
            catAnimController.LandedAnim();
            isOnJumpSequence = false;
            isJumpOnAir  = false;
        }
    }

    private void OnDrawGizmos() {
         
    }
}