using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RocketCollision : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1f;
    RocketMovement rocketMovementScript;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rocketMovementScript = GetComponent<RocketMovement>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other) {
        GameObject otherGObj = other.gameObject;
        if(otherGObj.tag == "Finish"){
            Debug.Log("Successes:Planet 1 is finished.");
            GoToNextPlanet(delayInSeconds);// go to next level/planet after 1 seconds
        }
        else if(otherGObj.tag == "Obstacles"){
             Debug.Log("Hitted an Obstacle.");
            OnCrashPlanet(delayInSeconds); 
        }
        else if(otherGObj.tag == "Ground"){
            Debug.Log("Reload Level");
           OnCrashPlanet(delayInSeconds);
        }
    }

    void OnCrashPlanet(float secondsInterval){
        rocketMovementScript.enabled = false;
         // add sound effect
        //add particle effect
        Invoke("ReloadActivePlanet",secondsInterval);
       
    }
    void GoToNextPlanet(float secondsInterval){
        rocketMovementScript.enabled = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        Invoke("NextPlanet", secondsInterval);
        
    }



    // go to the next planet
    void NextPlanet(){
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex+1)%SceneManager.sceneCountInBuildSettings;
        // go to next scene
        LoadGameScene(nextSceneIndex);
    }

    void ReloadActivePlanet(){
       LoadGameScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoadGameScene(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }
}
