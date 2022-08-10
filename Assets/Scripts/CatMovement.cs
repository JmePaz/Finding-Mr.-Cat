using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{   
    [SerializeField] float speed;
    CatAnimator catAnimController;


    // Start is called before the first frame update
    void Start()
    {
        catAnimController = GetComponentInChildren<CatAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            if(Input.GetKey(KeyCode.RightArrow)){
                MoveX(Vector3.right);
            }
            else if(Input.GetKey(KeyCode.LeftArrow)){
                MoveX(Vector3.left);
            }
        

            if(Input.GetKey(KeyCode.UpArrow)){
                MoveX(Vector3.forward);
            }
            else if(Input.GetKey(KeyCode.DownArrow)){
                MoveX(Vector3.back);
            }

        }
        else{
            catAnimController.IdleAnim();
        }
        
    }

    void MoveX(Vector3 direction){
        this.transform.Translate(direction*speed*Time.deltaTime);
        catAnimController.RunAnim();
    }
}
