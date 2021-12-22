﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int HP = 101;
    public GameObject coin;
    public EnemyLineManager enemyLineManager=null;

    public Material grayMat;

    public GameObject enemyObjectWithMat;

    private float rOffset, gOffset, bOffset;

    public Animator enemyAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        if (enemyLineManager != null)
        {
            enemyLineManager.incEnemies();
        }

        rOffset = grayMat.color.r - enemyObjectWithMat.GetComponent<Renderer>().material.color.r;
        gOffset = grayMat.color.g - enemyObjectWithMat.GetComponent<Renderer>().material.color.g;
        bOffset = grayMat.color.b - enemyObjectWithMat.GetComponent<Renderer>().material.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDmg(int dmg)
    {
        float dmgPercent = ((float)dmg) / HP;
        HP -= dmg;
        Color matColor= enemyObjectWithMat.GetComponent<Renderer>().material.color;
  
        enemyObjectWithMat.GetComponent<Renderer>().material.SetColor("_Color", new Color(matColor.r + rOffset * dmgPercent, matColor.g + gOffset * dmgPercent, matColor.b + bOffset * dmgPercent));

        rOffset -= rOffset * dmgPercent;
        gOffset -= gOffset * dmgPercent;
        bOffset -= bOffset * dmgPercent;
        //change color to more gray

        if (HP <= 0)
        {
            enemyObjectWithMat.GetComponent<Renderer>().material.SetTexture("_MainTex",null);
            //dying animation
            enemyAnimator.SetTrigger("Fall");
            //potential coin dropping

            //disable bezier curve follow
            transform.parent = null;


            enemyLineManager.decEnemies();
        //    Destroy(gameObject);  //destroy enemy for now
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
