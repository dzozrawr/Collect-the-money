using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineManager : MonoBehaviour
{
    private int enemiesInLine=0;
    public EnemyGroupManager enemyGroupManager=null;
    // Start is called before the first frame update
    void Start()
    {
        if (enemyGroupManager != null)
        {
            enemyGroupManager.incEnemyLines();
        }
    }

    // Update is called once per frame
    void Update()
    {
/*        for (int i = 0; i < transform.childCount; i++) //maybe the enemy could tell the line when its dead
        {
            if (i == 0) counter = 0;
            if (!transform.GetChild(i).CompareTag("Enemy")) counter++;
            if(i== (transform.childCount - 1))
            {

            } 
        }*/
    }

    public void incEnemies()
    {
        enemiesInLine++;
    }

    public void decEnemies()
    {
        enemiesInLine--;
        if (enemiesInLine == 0)
        {
            enemyGroupManager.destroyLine(gameObject);
           // enem
            //find offset between me and the line behind me
            //destroy this line
            //lerp forward each line by the amount of that offset
         
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
