using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Transform com;
    public Rigidbody rb;

    private float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //   rb = GetComponent<Rigidbody>();
        //  rb.centerOfMass = com.position;

        // transform.rotation = Quaternion.LookRotation(rb.velocity);

        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(rb.velocity),
                Time.deltaTime * rotationSpeed
            );
            // Debug.DrawLine(transform.position, rb.);
        }
    }
}
