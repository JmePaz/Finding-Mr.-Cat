using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{   

    [SerializeField] private Collider worldCollider;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject cat;
    [SerializeField] private Range gapYDist;
    [SerializeField] private Range xAxis;
    private float targetYAxis;

    private float curUpperLimitY;
    private float OnGameCapacity;
    private GameObject currentObstacle, currentCat;
    private CatObjectCounter catCounter;
    // Start is called before the first frame update
    void Start()
    {
        catCounter = GetComponent<CatObjectCounter>();

        targetYAxis = this.transform.position.y;
        Debug.Log(targetYAxis);

        //ignore world collider 
        int catLayer = (int)Mathf.Log(LayerMask.GetMask("Obstacle"),2);
        int worldLayer = (int)Mathf.Log(LayerMask.GetMask("World"),2);
        Physics.IgnoreLayerCollision(catLayer, worldLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObstacle == null || curUpperLimitY<= Mathf.Epsilon || (targetYAxis - currentObstacle.transform.position.y) 
            >= curUpperLimitY) {
            // create a new gap, pos , and an instance of obstacle game obj
            GenerateObs();
            //generate cat
            int randNum = Random.Range(0, 8);
            if(catCounter.CountCatOnScene < catCounter.CatLimit){
                GenerateCat();
                catCounter.Add();
            }

            //set the  obstacle obj to moving
            currentObstacle.GetComponent<ObstacleMovement>().enabled = true; 
        }
    }

    void GenerateObs(){
        curUpperLimitY = Random.Range(gapYDist.Min, gapYDist.Max);
        Vector3 position = new Vector3(Random.Range(xAxis.Min,xAxis.Max), targetYAxis, 0.0f);
        currentObstacle = Instantiate(obstacle, position, Quaternion.identity);
        currentObstacle.GetComponent<ObstacleMovement>().enabled = false; 
    }

    void GenerateCat(){
        //its scale
        Vector3 scale = Vector3.one * 4f;

        // its position
        Vector3 pos = currentObstacle.transform.position;
        pos.y += currentObstacle.transform.lossyScale.y/2;

        //its rotation
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        //generate object cat
        currentCat = Instantiate(cat, pos, rotation);
        currentCat.transform.localScale = scale;
        currentCat.transform.position += Vector3.up *(currentCat.transform.lossyScale.y*2.7f);


        //set to as a child and transfer as currLand
        currentCat.transform.SetParent(currentObstacle.transform);
        currentCat.GetComponent<CatAIMov>().currLand = currentObstacle;
    }
}
