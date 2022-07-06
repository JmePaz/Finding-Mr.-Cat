using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] private float thrustSpeed = 900f;
    [SerializeField] private float rotSpeed = 40f;
    [SerializeField] private AudioClip thrustAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        //components
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        //set audioSource to loop
        audioSource.loop = true;

    }

    // Update is called once per frame
    void Update()
    {
        //rotate 
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)){
            Rotate(Vector3.forward);
            Debug.Log("Rotating Left");
        }
        else if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)){
            Rotate(Vector3.back);
            Debug.Log("Rotating Right");
        }
        
        //thurst
        if(Input.GetKey(KeyCode.Space)){
            Thrust();
            PlayAudio();
            Debug.Log("Thrusting");
        }
        else{
            StopAudio();
        }
    }

    //rotate 
    void Rotate(Vector3 direction){
        rigidBody.freezeRotation = true; // freeze all rotation
        this.transform.Rotate(direction*rotSpeed*Time.deltaTime); //  rotate the rocket
        // freeze position z and rotation X, Y
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    //thurst upwards
    void Thrust(){
        rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }

    void PlayAudio(){
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(thrustAudioClip);
        }
    }
    
    void StopAudio(){
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
    }
}
