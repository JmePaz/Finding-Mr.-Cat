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
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;


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
        //side thrust
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)){
            rightThrustParticles.Play();  //add particles
            Rotate(Vector3.forward); //rotate
        }
        else if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)){
            leftThrustParticles.Play();  //add particles
            Rotate(Vector3.back); // rotate
        }
        else
        {
            StopSideParticles();
        }

        //upwards thurst
        if (Input.GetKey(KeyCode.Space)){
            Thrust();
        }
        else{
            mainThrustParticles.Stop();
            StopAudio();
        }
    }

    private void StopSideParticles()
    {
        //stop side thrust particles
        if (rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
        }
        else if (leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
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
        //particles
        if(!mainThrustParticles.isPlaying){
             mainThrustParticles.Play();
        }
        PlayAudio();
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

    public void StopAllParticles(){
        mainThrustParticles.Stop();
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }
}
