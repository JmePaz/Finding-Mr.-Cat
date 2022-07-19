
using System;
using UnityEngine;

[Serializable]
public class Range{
    [SerializeField] private float _min, _max;
    [SerializeField]public float Min { 
        get{ return _min; }
        set{
            if(_max<value){
                throw new Exception("Range is invalid");
            }
            _min = value;
        }
    }

    [SerializeField]public float Max { 
        get{
            return _max;
        }
        set{
            if(_min>value){
                throw new Exception("Range is invalid");
            }
            _max = value;
        }
    }

    // constructor
    public Range(float min = 0f,float max = 1f){
        if(max<min){ 
            throw new Exception("Range is invalid");
        }
        this.Min = min;
        this.Max = max;

    }
}