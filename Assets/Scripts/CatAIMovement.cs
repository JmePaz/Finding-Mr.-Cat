using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAIMovement: MonoBehaviour{

    CatAnimator catAnimController;
    Rigidbody myRigidBody;
    [SerializeField] float speed;
    bool isOnJumpSequence;
    bool isJumpOnAir;


    void Start()
    {
        catAnimController = GetComponent<CatAnimator>();
        myRigidBody = GetComponent<Rigidbody>();
        isOnJumpSequence = false;
        isJumpOnAir = false;
    }

    void Update(){

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
        if(isOnJumpSequence&&!isJumpOnAir && other.gameObject.name =="LaunchingPad"){
            Debug.Log("Has landed");
            catAnimController.LandedAnim();
            isOnJumpSequence = false;
            isJumpOnAir  = false;
        }
    }
}