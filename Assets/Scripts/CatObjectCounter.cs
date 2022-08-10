using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatObjectCounter : MonoBehaviour
{
    [SerializeField] private int _catLimit;
     private int _countCat;
    public int CatLimit {get{ return _catLimit;}  }
    public int CountCatOnScene {get{ return _countCat;}  }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Subtract(){
        
        _countCat =  Mathf.Max(0, _countCat-1);
    }

    public void Add(){
        _countCat++;
    }

    public void SetNewLimit(int value){
        if(value<0){
            throw new Exception("Value is invalid");
        }
        _catLimit = value;
    }
}
