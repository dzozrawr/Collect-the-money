using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingText : MonoBehaviour
{
    public float minScale = 0.75f;

    public float pulseSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // float changeRate= pulseSpeed * Time.deltaTime;
        if (transform.localScale.x >= 1f && pulseSpeed>0)
        {
            pulseSpeed = -pulseSpeed;
        }

        if(transform.localScale.x <= minScale && pulseSpeed < 0) pulseSpeed = -pulseSpeed;


        transform.localScale = new Vector3(transform.localScale.x+ pulseSpeed * Time.deltaTime, transform.localScale.y + pulseSpeed * Time.deltaTime, transform.localScale.z + pulseSpeed * Time.deltaTime);
        
    }
}
