using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour
{
    Animator catAnimController;
   public bool IsOnJumpSequence;
    // Start is called before the first frame update
    void Start()
    {
        catAnimController = GetComponent<Animator>();
        IsOnJumpSequence = false;
    }

    public void RunAnim(){
        if(!catAnimController.GetBool("isRunning")){
            catAnimController.SetBool("isRunning", true);
        }
    }
    public void IdleAnim(){
        if(catAnimController.GetBool("isRunning")){
            catAnimController.SetBool("isRunning", false);
        }
    }

    //jump sequence
    public void JumpOnAirAnim(){
        catAnimController.SetTrigger("jumpOnAir");
        IsOnJumpSequence = true;
    }

    public void JumpDownAnim(){
        catAnimController.SetTrigger("jumpDown");
    }

    public void LandedAnim(){
        catAnimController.SetTrigger("landed");
        IsOnJumpSequence = false;
    }

    public void ResetAllTriggers(){
        catAnimController.ResetTrigger("jumpOnAir");
        catAnimController.ResetTrigger("jumpDown");
        catAnimController.ResetTrigger("landed");
    }


}
