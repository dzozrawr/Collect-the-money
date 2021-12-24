using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeScript : MonoBehaviour
{
    Transform enemyGroup;

    int numOfPhases = 5, curDestructPhase=0;
    float destructionPhaseDur = 3f; //was 0.25f

    public GameObject theModelParent;

    public GameObject[] theModel;

    public Material[] matPhases;

    private PlayerController playerController;
    private GameController gameController;

    public GameObject theDoor;

    private float timeToDie = 6f;
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

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
        if (curDestructPhase < matPhases.Length)
        {
            for (int i = 0; i < theModelParent.transform.childCount; i++)
            {
                theModelParent.transform.GetChild(i).GetComponent<Renderer>().material = matPhases[curDestructPhase];
            }
          //  theModel.GetComponent<Renderer>().material = matPhases[curDestructPhase];
        }

        curDestructPhase++;
     //   Debug.Log(curDestructPhase);
        if(curDestructPhase== numOfPhases)
        {

            //explode barricade
            for (int i = 0; i < theModelParent.transform.childCount; i++)
            {
                theModelParent.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            }

            enemyGroup.GetComponent<EnemyGroupManager>().setDefaultSpeed(); //change enemies speed
            playerController.GoToDefaultState();

            //disable the trigger collider that stops the enemies
            gameObject.GetComponent<Collider>().enabled = false;

            //destroy the door
            Destroy(theDoor);
            // destroy barricade after a while
            Destroy(gameObject, timeToDie);

            //disable cta shoot them now
            gameController.disableShootThemNowText();
            return;
        }
        Invoke(nameof(destroyPhase), destructionPhaseDur);
    }
}
