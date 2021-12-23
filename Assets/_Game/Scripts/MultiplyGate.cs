using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyGate : MonoBehaviour
{
    public static int globalGateId=0;
    private int gateId = globalGateId++;

    public int multiplyBy = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))  //can be more general, for example a "Projectile" tag
        {

          //  Debug.Log("other.CompareTag(Arrow)");
            GameObject arrowGo = other.gameObject;

            if(arrowGo.GetComponent<Projectile>().addCollidedGateId(gateId))    //return true if projectile didnt collide with the gate before
            {
                for (int i = 0; i < (multiplyBy-1); i++)
                {
                    GameObject newArrow = Instantiate(arrowGo, arrowGo.transform.position + Random.insideUnitSphere, arrowGo.transform.rotation);
                    newArrow.GetComponent<Projectile>().addCollidedGateId(gateId);
                    newArrow.GetComponent<Rigidbody>().velocity = arrowGo.GetComponent<Rigidbody>().velocity;
                }
            }

           
            /*            GameObject arrowGo = other.gameObject;

                        for (int i = 0; i < multiplyBy; i++)
                        {
                            Instantiate(arrowGo, arrowGo.transform.position + Random.insideUnitSphere, arrowGo.transform.rotation);
                        }            
                        */
        }
    }
}
