using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Range gapYDist;
    [SerializeField] private Range xAxis;
    private float targetYAxis;

    private float curUpperLimitY;
    private float OnGameCapacity;
    private GameObject currentObstacle;
    // Start is called before the first frame update
    void Start()
    {
        targetYAxis = this.transform.position.y;
        Debug.Log(targetYAxis);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObstacle == null || curUpperLimitY<= Mathf.Epsilon || (targetYAxis - currentObstacle.transform.position.y) 
            >= curUpperLimitY) {
            // create a new gap, pos , and an instance of obstacle game obj
            curUpperLimitY = Random.Range(gapYDist.Min, gapYDist.Max);
            Vector3 position = new Vector3(Random.Range(xAxis.Min,xAxis.Max), targetYAxis, 0.0f);
            currentObstacle = Instantiate(obstacle, position, Quaternion.identity);
        }
    }
}
