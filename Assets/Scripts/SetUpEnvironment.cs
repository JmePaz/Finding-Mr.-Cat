using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpEnvironment : MonoBehaviour
{
    RocketMovement rocketMovementScript;
    [SerializeField] ObstacleMovement LauncingPadInitator;
    [SerializeField] InitateObstacles obstacleGenerator;
    // Start is called before the first frame update
    void Start()
    {
        rocketMovementScript = GetComponent<RocketMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rocketMovementScript.isThrusting){
            //disengaged launching pad and start generating obstacles
            this.enabled = false;
            LauncingPadInitator.enabled = true;
            obstacleGenerator.Activate();
            Destroy(this);
        }
    }
}
