using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;

    [SerializeField] private float thrustSpeed = 9f;
    [SerializeField] private float rotSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        //rotate 
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)){
            Rotate(Vector3.forward);
            Debug.Log("Rotating Left");
        }
        else if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)){
            Rotate(Vector3.back);
            Debug.Log("Rotating Right");
        }
        
        //thurst
        if(Input.GetKey(KeyCode.Space)){
            Thrust();
            Debug.Log("Thrusting");
        }
    }

    //rotate 
    void Rotate(Vector3 direction){
        rigidBody.freezeRotation = true;
        this.transform.Rotate(direction*rotSpeed*Time.deltaTime);
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    //thurst upwards
    void Thrust(){
        rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }
}
