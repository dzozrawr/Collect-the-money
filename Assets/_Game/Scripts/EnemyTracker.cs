using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public Transform enemyGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyGroup == null)
        {
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.LookAt(enemyGroup);
        }
        
    }
}
