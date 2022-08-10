using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCollision : MonoBehaviour
{
    [SerializeField] CatObjectCounter catCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        GameObject otherGObj = other.gameObject;

        if(otherGObj.name == "End"){
            catCounter.Subtract();
            Destroy(this);
        }
    }
}
