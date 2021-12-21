using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    public GameObject coin;
    public EnemyLineManager enemyLineManager=null;
    // Start is called before the first frame update
    void Start()
    {
        if (enemyLineManager != null)
        {
            enemyLineManager.incEnemies();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDmg(int dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            //dying animation
            //potential coin dropping
            //disable bezier curve follow

            //  GameObject go=  Instantiate(coin, transform.position,Quaternion.identity);    //coin part
            //  go.transform.parent= transform.parent;
            enemyLineManager.decEnemies();
            Destroy(gameObject);  //destroy enemy for now
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("OnTriggerEnter(Collider other)");
        if (other.gameObject.CompareTag("Barricade"))
        {
            Transform enemyGroup = transform.parent.parent;
            // Debug.Log("OnTriggerEnter(Collider other)");
            enemyGroup.GetComponent<EnemyGroupManager>().setSpeed(0f);    //
            other.gameObject.GetComponent<BarricadeScript>().initiateBarricadeDestruction(enemyGroup);
        }
    }

}
