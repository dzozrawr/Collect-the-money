using System.Collections;
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

    private PlayerController playerController;

    private float timeToDie = 6f;

    public ParticleSystem coinExplosion;
    
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

        playerController=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDmg(int dmg)
    {
        if (HP <= 0) return;

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

            gameObject.GetComponent<Collider>().enabled = false;

            //delete all the arrows on enemy's body and die after a while (for now)
            DyingSequence();
            //    Destroy(gameObject);  //destroy enemy for now
        }
    }

    private void DyingSequence()
    {
        //coin explosion
        coinExplosion.Play();

        for (int i = 0; i < transform.childCount; i++)  //delete all arows in the body
        {
            if (transform.GetChild(i).CompareTag("Arrow"))
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        Destroy(gameObject, timeToDie);
    }

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("OnTriggerEnter(Collider other)");
        if (other.gameObject.CompareTag("Barricade"))
        {
            Transform enemyGroup = transform.parent.parent;
            // Debug.Log("OnTriggerEnter(Collider other)");
            enemyGroup.GetComponent<EnemyGroupManager>().setSpeed(0f);    //stop the whole group
            playerController.GoToBarricadeState();

            other.gameObject.GetComponent<BarricadeScript>().initiateBarricadeDestruction(enemyGroup);
        }
    }

}
