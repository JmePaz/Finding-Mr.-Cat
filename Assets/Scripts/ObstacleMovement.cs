using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{   
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(this.transform.position.y<=-200f){
            Destroy(this.gameObject);
        }

        this.transform.Translate(Vector3.down*speed*Time.deltaTime);
    }

    public void Stop(){
        speed = 0f;
    }
}
