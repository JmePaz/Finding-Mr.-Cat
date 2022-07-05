using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        GameObject otherGObj = other.gameObject;
        if(otherGObj.tag == "Finish"){
            Debug.Log("Successes:Planet 1 is finished.");
        }
        else if(otherGObj.tag == "Obstacles"){
             Debug.Log("Hitted an Obstacle.");
        }
    }
}
