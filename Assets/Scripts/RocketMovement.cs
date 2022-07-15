using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] private float thrustSpeed = 900f;
    [SerializeField] private float speed = 40f;
    [SerializeField] private AudioClip thrustAudioClip;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    bool isThrusting;

    // Start is called before the first frame update
    void Start()
    {
        //components
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        //set audioSource to loop
        audioSource.loop = true;
        //stop rotation
         rigidBody.freezeRotation = true; // freeze all rotation
        isThrusting = false;
    }

    // Update is called once per frame
    void Update()
    {
        //side thrust
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)){
            rightThrustParticles.Play();  //add particles
           // Rotate(Vector3.forward); //rotate
            Move(Vector3.left);
        }
        else if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)){
            leftThrustParticles.Play();  //add particles
            //Rotate(Vector3.back); // rotate
            Move(Vector3.right);
        }
        else
        {
            StopSideParticles();
        }

        //upwards thurst
        if (Input.GetKey(KeyCode.Space)){
            isThrusting = true;
            Thrust();
        }
        else{
            isThrusting = false;
            mainThrustParticles.Stop();
            StopAudio();
        }
    }
    void FixedUpdate(){
        //velocity!=0
       if(isThrusting && rigidBody.velocity.y<Mathf.Epsilon){
            rigidBody.velocity = Vector3.up * speed * Time.deltaTime;
       }
       else if(!isThrusting&&rigidBody.velocity.y>=Mathf.Epsilon){
            rigidBody.velocity = Vector3.down * speed*2f *Time.deltaTime;
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
        this.transform.Rotate(direction*speed*Time.deltaTime); //  rotate the rocket
        // freeze position z and rotation X, Y
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    void Move(Vector3 direction){
        this.transform.Translate(direction*speed*2f*Time.deltaTime);
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
