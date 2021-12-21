using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeScript : MonoBehaviour
{
    Transform enemyGroup;

    int numOfPhases = 3, curDestructPhase=0;
    float destructionPhaseDur = 0.25f;

    public GameObject theModel;

    public Material[] matPhases;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //theModel.GetComponent<Renderer>().material
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
     //   Debug.Log("OnTriggerEnter");
    }

    public void initiateBarricadeDestruction(Transform enemyG)
    {
        enemyGroup = enemyG;
        Invoke(nameof(destroyPhase), destructionPhaseDur);
    }

    private void destroyPhase()
    {
        //change texture
        if(curDestructPhase< matPhases.Length) theModel.GetComponent<Renderer>().material = matPhases[curDestructPhase];
        curDestructPhase++;
     //   Debug.Log(curDestructPhase);
        if(curDestructPhase== numOfPhases)
        {

            //explode barricade
                
            enemyGroup.GetComponent<EnemyGroupManager>().setDefaultSpeed(); //change enemies speed
            Destroy(gameObject);  // destroy barricade
            return;
        }
        Invoke(nameof(destroyPhase), destructionPhaseDur);
    }
}
