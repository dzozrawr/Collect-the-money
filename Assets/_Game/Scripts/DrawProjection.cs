using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    PlayerController playerController;
    LineRenderer lineRenderer;

    public int numPoints = 50;

    public float timeBetweenPoints = 0.1f;

    public LayerMask CollidableLayers;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();

        Vector3 startingPosition = playerController.shootPoint.position;
        Vector3 startingVelocity = playerController.shootPoint.forward * playerController.shootingPower;

        for (float t = 0; t < numPoints; t+=timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            //
        }

        lineRenderer.SetPositions(points.ToArray());
    }
}
