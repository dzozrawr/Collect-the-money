using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    private bool isClosing = false;
    public GameObject theDoor;

    private float closingSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClosing&& (theDoor!=null))
        {
           // Debug.Log("transform.localRotation.eulerAngles.y=" + transform.localRotation.eulerAngles.y);
         //   float newYRot = theDoor.transform.localRotation.eulerAngles.y - closingSpeed * Time.deltaTime;      //we do a little bit of hard coding (should've find the offset or set the desired angle)
          //  Debug.Log("newYRot=" + newYRot);


            float newYRot= Mathf.Lerp(theDoor.transform.localRotation.eulerAngles.y, 0, closingSpeed * Time.deltaTime);

            if (newYRot < 0.5f)
            {
                newYRot = 0f;
                isClosing = false;
            }

            theDoor.transform.localRotation = Quaternion.Euler(theDoor.transform.localRotation.eulerAngles.x, newYRot, theDoor.transform.localRotation.eulerAngles.z);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isClosing == true) return;
            isClosing = true;
        }
    }
}
