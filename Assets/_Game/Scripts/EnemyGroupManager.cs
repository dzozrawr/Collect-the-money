using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class EnemyGroupManager : MonoBehaviour
{
    private int lineCount = 0;
    float lineOffset;
    private PathFollower pathFollower;

    private float defaultSpeed;
    // Start is called before the first frame update
    void Start()
    {
        pathFollower=gameObject.GetComponent<PathFollower>();
        defaultSpeed = pathFollower.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incEnemyLines()
    {
        lineCount++;
    }

    public void setSpeed(float speed)
    {
        pathFollower.speed = speed;
    }

    public void setDefaultSpeed()
    {
        pathFollower.speed = defaultSpeed;
    }

    public void destroyLine(GameObject enemyLine)
    {
        //find offset between me and the line behind me
        int enemyLineIndex= transform.childCount-1;
       // Debug.Log("enemyLineIndex="+ enemyLineIndex);
        // if ((GameObject.ReferenceEquals(transform.GetChild(enemyLineIndex).gameObject, enemyLine.gameObject)) &&(enemyLineIndex!=0))
        if ((transform.GetChild(enemyLineIndex).gameObject.GetInstanceID()== enemyLine.GetInstanceID()) && (enemyLineIndex != 0))
        {
            lineOffset = transform.GetChild(enemyLineIndex - 1).transform.localPosition.z- enemyLine.transform.localPosition.z ;
          //  Debug.Log("LineOffset=" + lineOffset);
        }

        /*        for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i) == enemyLine) enemyLineIndex = i; //this is for moving when ANY line is destroyed
                }*/

        //destroy this line
        enemyLine.GetComponent<EnemyLineManager>().Destroy();

        //lerp forward each line by the amount of that offset
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<EnemyLineLerper>().setLineOffset(lineOffset);
        }

        lineCount--;
        if (lineCount == 0)
        {
            //do stuff
            Destroy(gameObject);
        }
    }
}
