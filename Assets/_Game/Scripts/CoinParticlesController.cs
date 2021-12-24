using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParticlesController : MonoBehaviour
{
    private ParticleSystem coinParticles;
    // private GameObject player;
    private GameObject coinDest;
    // Start is called before the first frame update
    void Start()
    {
        coinParticles = GetComponent<ParticleSystem>();

        //player=  GameObject.FindGameObjectWithTag("Player");
        coinDest = GameObject.FindGameObjectWithTag("CoinDestination");
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 forceVector=  coinDest.transform.position;
      var force=  coinParticles.forceOverLifetime;
        /*        force.x = -player.transform.position.x;
                force.y = -player.transform.position.y;
                force.z = -player.transform.position.z;*/
        /*        force.x = forceVector.transform.position.x;
                force.y = forceVector.transform.position.y;
                force.z = 0;*/
        force.x = forceVector.x;
        force.y = forceVector.y;
        force.z = forceVector.z;

        Debug.DrawLine(transform.position, forceVector);
    }


}
