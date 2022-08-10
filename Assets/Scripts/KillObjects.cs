using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjects : MonoBehaviour
{

    [SerializeField]CatObjectCounter catCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        string tag = other.gameObject.tag;
        if(other.gameObject.tag == "Cat"){
            catCounter.Subtract();     
        }
        Destroy(other.gameObject);
    }
}
