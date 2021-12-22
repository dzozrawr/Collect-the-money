using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;

    private float rotationSpeed = 10f;

    [SerializeField] private int dmg = 25;  //basic enemy HP is 100

    private float timeForDestruction = 2f;
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Enemy"))
        {
           // Debug.Log("other.CompareTag(Enemy)");
            gameObject.transform.parent = other.transform;
            rb.isKinematic = true;

            other.GetComponent<EnemyScript>().takeDmg(dmg);
        }

        if (other.CompareTag("Floor"))
        {
            

            rb.isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);

            Invoke(nameof(SelfDestruct), timeForDestruction);
          //  other.GetComponent<EnemyScript>().takeDmg(dmg);
        }
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
