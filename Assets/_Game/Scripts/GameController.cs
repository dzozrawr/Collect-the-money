using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject shootThemNowText;
    public Text coinsText;
    private void Awake()
    {
        Time.timeScale = 3f;
        Debug.Log("Time.timeScale =" + Time.timeScale);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        int coinCount = int.Parse(coinsText.text);
        coinCount += coins;
        coinsText.text = coinCount + "";
    }
}
