using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class PlayerController : MonoBehaviour
{
    public float shootingPower = 5f;

    public GameObject arrow;
    public Transform shootPoint;

    public Vector3 shootPointDefaultRot;

    public float aimingRotSpeed = 20f;
    public float rotLimit = -70f;

    public float shootingPowerIncreaseRate = 20f;
    public float shootingPowerLimit = 70f;
    public float shootingDefaultValue;

    public PathFollower pathFollower;
    public PathCreator pathCreator;

    private float defaultSpeed;

    public Animator playerAnimator;

    public GameObject theArrowCharacterIsHolding;

    private bool isDefaultState = true, isBarricadeState = false;
    // Start is called before the first frame update
    void Start()
    {
        shootPointDefaultRot = shootPoint.localRotation.eulerAngles;
        defaultSpeed = pathFollower.speed;
        transform.position = pathCreator.path.GetPointAtTime(0.8f);
        pathFollower.SetClosestDistanceAlongPath();

        shootingDefaultValue = shootingPower;
      //  transform.position = pathCreator.path.GetPointAtDistance(5f);
    }

    // Update is called once per frame
    void Update()
    {
       if(isDefaultState&&!isBarricadeState)  DefaultState();
       if(!isDefaultState && isBarricadeState) BarricadeState();
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

    private void DefaultState()
    {
        if (Input.GetMouseButton(0))
        {
            pathFollower.speed = 0f;
            playerAnimator.SetBool("Shoot", false);
            playerAnimator.SetBool("Shoot", true);
            theArrowCharacterIsHolding.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            pathFollower.speed = defaultSpeed;
            playerAnimator.SetTrigger("Run");
            playerAnimator.SetBool("Shoot", false);
            theArrowCharacterIsHolding.SetActive(false);
        }
    }

    private void BarricadeState()
    {
        if (Input.GetMouseButton(0))
        {
            playerAnimator.SetBool("Shoot", true);
            theArrowCharacterIsHolding.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
          //  playerAnimator.SetTrigger("Run");
            playerAnimator.SetBool("Shoot", false);
            theArrowCharacterIsHolding.SetActive(false);
        }
    }

    public void SetSpeed(float s)   //change the name of the method
    {
        pathFollower.speed = 0f;
       // playerAnimator.SetTrigger("TurnAround");
       //need to make special animations for turn around and then shooting
    }

    public void GoToBarricadeState()
    {
        pathFollower.speed = 0f;
        playerAnimator.SetBool("Shoot",true);
        isBarricadeState = true;
        isDefaultState = false;
    }

    public void GoToDefaultState()
    {
        pathFollower.speed = defaultSpeed;
        playerAnimator.SetBool("Shoot", false);
        playerAnimator.SetTrigger("Run");
        isBarricadeState = false;
        isDefaultState = true;
    }

    public void SetDefaultSpeed()
    {
        pathFollower.speed = defaultSpeed;
    }
}
