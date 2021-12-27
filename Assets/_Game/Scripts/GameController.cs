using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using Tabtale.TTPlugins;

public class GameController : MonoBehaviour
{
    public GameObject shootThemNowText;
    public Text coinsText;
    private int coinsToAdd=0;
    private bool addingCoins = false;

    private PlayerController playerController;

    public GameObject crowdPrefab;

    private float crowdDistanceFromPlayer = 15f;

    public Camera mainCamera;

    public GameObject defaultUI, victoryScreen, gameOverScreen;

    private int coinsAddedPerFrame = 5;

    private int actualCoinsPerFrame;

    private EnemyGroupManager enemyGroupManager;

    private bool isGameOver = false, isVictory=false;
    private void Awake()
    {
        TTPCore.Setup();

        Time.timeScale = 3f;
       // Debug.Log("Time.timeScale =" + Time.timeScale);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyGroupManager = GameObject.FindGameObjectWithTag("EnemyGroup").GetComponent<EnemyGroupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coinsToAdd > 0)
        {
            actualCoinsPerFrame = coinsAddedPerFrame;
            if (coinsToAdd< coinsAddedPerFrame)
            {
                actualCoinsPerFrame = coinsToAdd;
            }
            coinsToAdd-= actualCoinsPerFrame;
            int coinCount = int.Parse(coinsText.text);
            coinCount+= actualCoinsPerFrame;
            coinsText.text = coinCount + "";
        }
    }

    public void enableShootThemNowText()
    {
        shootThemNowText.SetActive(true);
    }


    public void disableShootThemNowText()
    {
        shootThemNowText.SetActive(false);
    }

    public void addCoinsToScore(int coins)
    {
        coinsToAdd += coins;
    }

    public void PlayVictorySequence()
    {
        //player speed=0
        playerController.SetSpeed(0);

        
        
        Vector3 inFrontOfV = -playerController.gameObject.transform.forward * crowdDistanceFromPlayer;  //take the players forward vector and go a few units in front of it
        inFrontOfV = playerController.transform.position + inFrontOfV;
        GameObject newCrowd= Instantiate(crowdPrefab, inFrontOfV, Quaternion.identity); //spawn people in front of player
        newCrowd.transform.LookAt(playerController.gameObject.transform);   //people look at player

        //play players throwing animation
        playerController.playerAnimator.SetTrigger("Throw");
        //play coin particles
        playerController.PlayCoinsThrow();
        //transition the camera to a new place
        //disable cinemachine
        mainCamera.GetComponent<CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraTransitions>().transitionCameraToVictory();


        //disable default UI
        defaultUI.SetActive(false);

        //enable UI victory message and play again button (next level in the possible future)
        victoryScreen.SetActive(true);

        isGameOver = true;
    }

    public void PlayGameOverSequence()
    {
        if (isGameOver) return;
        isGameOver = true;
        //player speed=0
        playerController.SetSpeed(0);

        //enemy group speed=0
        enemyGroupManager.setSpeed(0);

        //play falling animation once
        playerController.playerAnimator.SetTrigger("Fall");

        //play cheer animation for enemies


        //play coin particles

        //disable default UI
        defaultUI.SetActive(false);

        //enable game over UI
        gameOverScreen.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void Retry()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    /*    public void addCoinsToScore(int coins)
        {
            int coinCount = int.Parse(coinsText.text);
            coinCount += coins;
            coinsText.text = coinCount + "";
        }*/
}
