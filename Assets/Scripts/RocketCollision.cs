using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class RocketCollision : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float delayInSeconds = 1f;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    RocketMovement rocketMovementScript;
    Rigidbody rigidBody;

    public bool isInCollision, isCollisionEnabled;

    // Start is called before the first frame update
    void Start()
    {
        rocketMovementScript = GetComponent<RocketMovement>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        isInCollision = false;
        isCollisionEnabled = true;
    
    }
    void Update()
    {

    }

    private void CheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GoToNextPlanet(0.6f);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isCollisionEnabled = !isCollisionEnabled;
            Debug.Log("isCollisionEnabled");
        }
  
    }

    void OnCollisionEnter(Collision other) {
        if(isInCollision || !isCollisionEnabled){
            return;
        }

        GameObject otherGObj = other.gameObject;
        if(otherGObj.tag == "Cat"){
            Debug.Log("Successes:Planet 1 is finished.");
            Destroy(otherGObj);
            GoToNextPlanet(delayInSeconds);// go to next level/planet after 1 seconds
            isInCollision = true;
        }
        else if(otherGObj.tag == "Obstacles"){
            //stop obs movements
            BounceOff(other.contacts[0].point.normalized);
            //play particles and sound sfx
             Debug.Log("Hitted an Obstacle.");
            OnCrashPlanet(delayInSeconds); 
            //already collided
            isInCollision = true;
        }
        else if(otherGObj.tag == "Ground"){
            Debug.Log("Reload Level");
           OnCrashPlanet(delayInSeconds);
           isInCollision = true;
        }
        else if(otherGObj.name == "End"){
            Debug.Log("Reload Level");
            OnCrashPlanet(delayInSeconds);
            isInCollision = true;
        }
    }

    void OnCrashPlanet(float secondsInterval=1f){
        //modify rocket movements
        SetCameraViewToNothing();
    
        rocketMovementScript.StopAllParticles();
        rocketMovementScript.SpeedDown();
        //rocketMovementScript.UnFreezeRotation();
        rocketMovementScript.enabled = false;
        
         // add sound effect
         audioSource.Stop();
         audioSource.PlayOneShot(deathExplosion, 0.4f);
        //add particle effect
        explosionParticles.Play();
        //reload planet
        Invoke("ReloadActivePlanet",secondsInterval);
       
    }

    void SetCameraViewToNothing(){
        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;
    }
    void GoToNextPlanet(float secondsInterval=1f){
        //restrain movements
        rocketMovementScript.StopAllParticles();
        rocketMovementScript.enabled = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        //play success
         audioSource.Stop();
         audioSource.PlayOneShot(success, 0.8f);
        //add particle effect
        successParticles.Play();
        //next planet
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

    void StopObstaclesMovement(){
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        foreach(GameObject obstacle in obstacles){
            ObstacleMovement obsMovScript = obstacle.GetComponent<ObstacleMovement>();
            obsMovScript.Stop();
        }
    }

    void BounceOff(Vector3 contactPoint){
        Vector3 direction  = (contactPoint - this.transform.position).normalized;
        if(direction.y<=0){
            direction.y = 1;
        }
        rigidBody.AddForce(-direction*100, ForceMode.VelocityChange);
    }
}
