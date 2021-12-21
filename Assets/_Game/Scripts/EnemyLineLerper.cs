using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineLerper : MonoBehaviour
{
    [SerializeField] private float transitionSpeed = 3f;

    private Vector3 destination;
    private bool isTransitionAcitve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransitionAcitve)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime * transitionSpeed);
          //  Debug.Log(transform.localPosition);
        }
    }

    public void setLineOffset(float offset)
    {
        destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z-offset);
        isTransitionAcitve = true;
    }

}
