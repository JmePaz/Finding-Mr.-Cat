using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitateObstacles : MonoBehaviour
{
    GenerateObstacles obstacleGenerator;
    // Start is called before the first frame update
    void Start()
    {
        obstacleGenerator = GetComponent<GenerateObstacles>();
    }

   public void Activate(){
    obstacleGenerator.enabled =  true;
   }
}
