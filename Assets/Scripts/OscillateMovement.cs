using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateMovement: MonoBehaviour
{
    const float TAU = Mathf.PI * 2;
    private Vector3 startPos;
    [SerializeField]private Vector3 distance;
    [SerializeField][Range(0,1)] private float movFactor;
    [SerializeField][Range(1f,50f)] private float period = 2f;
    
    
    // Start is called before the first frame update
    void Start()
    {   
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //period cannot be zero
        if(period<=Mathf.Epsilon){
            return;
        }

        // sin waves calculations
        float cycles = Time.time/period;
        float sinWave = Mathf.Sin(cycles * TAU);
        float sinWaveFactor = (sinWave+1)%2;
        movFactor = sinWaveFactor;
        // implement thru transform
        Vector3 movOffset = distance * sinWaveFactor;
        this.transform.position = startPos +movOffset;

    }
}
