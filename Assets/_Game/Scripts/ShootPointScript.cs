using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController playerController;
    LineRenderer lineRenderer;

    public int numPoints = 50;

    public float timeBetweenPoints = 0.1f;

    public LayerMask CollidableLayers;

    private GameObject enemyTracker=null;
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;

        enemyTracker = GameObject.FindGameObjectWithTag("EnemyTracker");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTracker != null)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, enemyTracker.transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }

        if (Input.GetMouseButton(0))
        {
            if (!lineRenderer.enabled)
            {
                playerController.shootingPower = playerController.shootingDefaultValue;
                lineRenderer.enabled = true;
            }


            float newShootingPower = playerController.shootingPower + playerController.shootingPowerIncreaseRate * Time.deltaTime;

            if (newShootingPower <= playerController.shootingPowerLimit) playerController.shootingPower = newShootingPower;           //changing velocity



            // transform.rotation = Quaternion.Euler(newXVal, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;

            }

            GameObject newArrow = Instantiate(playerController.arrow, transform.position, transform.rotation);
            newArrow.GetComponent<Rigidbody>().velocity = transform.forward * playerController.shootingPower;


        }


        if (lineRenderer.enabled)
        {

            lineRenderer.positionCount = numPoints;
            List<Vector3> points = new List<Vector3>();

            Vector3 startingPosition = transform.position;
            Vector3 startingVelocity = transform.forward * playerController.shootingPower;

            for (float t = 0; t < numPoints; t += timeBetweenPoints)
            {
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;

                if (Physics.OverlapSphere(newPoint, 0.1f, CollidableLayers).Length > 0)
                {
                    lineRenderer.positionCount = points.Count;
                    break;
                }

                points.Add(newPoint);

                //
            }

            lineRenderer.SetPositions(points.ToArray());
        }


        /*        if (Input.GetMouseButton(0))
                {
                    if (!lineRenderer.enabled) lineRenderer.enabled = true;

                    float newXVal = transform.rotation.eulerAngles.x - playerController.aimingRotSpeed * Time.deltaTime;

                    if (((newXVal - 360f) < playerController.rotLimit) && ((newXVal - 360f) > -360f)) newXVal = playerController.rotLimit;      //changing angle



                    transform.rotation = Quaternion.Euler(newXVal, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

                }


                if (Input.GetMouseButtonUp(0))
                {
                    if (lineRenderer.enabled) lineRenderer.enabled = false;

                    GameObject newArrow = Instantiate(playerController.arrow, transform.position, transform.rotation);
                    newArrow.GetComponent<Rigidbody>().velocity = transform.forward * playerController.shootingPower;

                    //  transform.rotation = Quaternion.Euler(playerController.shootPointDefaultRot);
                    transform.localRotation = Quaternion.Euler(playerController.shootPointDefaultRot);
                }*/



    }
}
