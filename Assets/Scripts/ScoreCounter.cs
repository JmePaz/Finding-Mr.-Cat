using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    RocketCollision rocketCollisionScript;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    private float totalAltitude;
    void Start()
    {
        totalAltitude = 0;
        rigidBody = GetComponent<Rigidbody>();
        rocketCollisionScript = GetComponent<RocketCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!rocketCollisionScript.isInCollision){
            float currAlt = CalcAltitude(Time.deltaTime)/100;
            totalAltitude += currAlt;
            string textDisplay = string.Format("Altitude: {0:0.00}m",totalAltitude);
            scoreText.SetText(textDisplay);
        }
    }

    float CalcAltitude(float time){

        float veloc = rigidBody.velocity.y;
        //degrade
        if(Mathf.Abs(veloc)<1f){
            veloc = 0f;
        }
        else if(veloc<Mathf.Epsilon){
            veloc /= 10f;
        }

        return Mathf.Abs(veloc)*time;
    }
}
