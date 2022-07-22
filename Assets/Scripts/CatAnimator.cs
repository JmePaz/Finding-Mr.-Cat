using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour
{
    Animator catAnimController;
    bool isOnJumpSequence;
    // Start is called before the first frame update
    void Start()
    {
        catAnimController = GetComponent<Animator>();
        isOnJumpSequence = false;
    }

    public void RunAnim(){
        catAnimController.SetBool("isRunning", true);
    }
    public void IdleAnim(){
        catAnimController.SetBool("isRunning", false);
    }

    //jump sequence
    public void JumpOnAirAnim(){
        catAnimController.SetTrigger("jumpOnAir");
    }

    public void JumpDownAnim(){
        catAnimController.SetTrigger("jumpDown");
    }

    public void LandedAnim(){
        catAnimController.SetTrigger("landed");
    }
}
