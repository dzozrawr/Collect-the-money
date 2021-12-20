using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class PlayerController : MonoBehaviour
{
    public float shootingPower = 5f;

    public GameObject arrow;
    public Transform shootPoint;

    public Vector3 shootPointDefaultRot;

    public float aimingRotSpeed = 20f;
    public float rotLimit = -70f;

    public PathFollower pathFollower;

    private float defaultSpeed;
    // Start is called before the first frame update
    void Start()
    {
        shootPointDefaultRot = shootPoint.localRotation.eulerAngles;
        defaultSpeed = pathFollower.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            pathFollower.speed = 0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pathFollower.speed = defaultSpeed;
        }
        /*        
                {
                    float newXVal = shootPoint.rotation.eulerAngles.x - aimingRotSpeed * Time.deltaTime;

                    if (((newXVal-360f) < rotLimit) && ((newXVal - 360f)>-360f)) newXVal = rotLimit;



                    shootPoint.rotation = Quaternion.Euler(newXVal, shootPoint.rotation.eulerAngles.y, shootPoint.rotation.eulerAngles.z);


                }

                if (Input.GetMouseButtonUp(0))
                {
                    GameObject newArrow = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
                    newArrow.GetComponent<Rigidbody>().velocity = shootPoint.transform.forward * shootingPower;

                    shootPoint.rotation = Quaternion.Euler(shootPointDefaultRot);
                }*/
    }
}
