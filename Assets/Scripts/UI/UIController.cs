using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public int score, collectibles;
    public TextMeshProUGUI scoreText, collectibleText;

    //public float lifeAmount = 100f;
    //float maxLifeAmount;

    //public float lifeLossAmount;
    //public float lifeLossSpeed = 5f;

    //public Image lifeBar;

    public GameObject optionsScreen, settingsScreen, winScreen;
    UltimatePlayerController playerRef;
    public bool canAccessOptions;

    public bool canAccessTutorials;

    public Button mainOptionButton;
    public bool isSettingsOn;

    private void Start()
    {

        score = 0;
        if(scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        //maxLifeAmount = lifeAmount;

        playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
    }

    private void Update()
    {
        //if(lifeBar != null)
        //{
        //    UpdateLifeBar();
        //}

        //if(lifeLossAmount > 0)
        //{
        //    lifeLossAmount -= lifeLossSpeed * Time.deltaTime;
        //    lifeAmount -= lifeLossSpeed * Time.deltaTime;
        //}

        if (Input.GetKeyDown(KeyCode.Escape) && canAccessOptions) 
        {
            if(isSettingsOn)
            {
                isSettingsOn = false;
                mainOptionButton.Select();
                settingsScreen.SetActive(false);
            }
            else
            {
                OptionsScreen();
            }
        }

        //if(Input.GetKeyDown(KeyCode.F1) || lifeAmount <= 0f || collectibles >= 3)
        //{
        //    WinScreen();
        //}
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;

        scoreText.text = score.ToString();
    }

    public void UpdateCollectibles()
    {
        collectibles++;

        collectibleText.text = collectibles.ToString();
    }

    //void UpdateLifeBar()
    //{
    //    lifeAmount -= Time.deltaTime;

    //    lifeBar.fillAmount = lifeAmount / maxLifeAmount;
    //}

    //public void LifeBarLossAnimation(float amount)
    //{
    //    lifeLossAmount = amount;
    //}

    public void OptionsScreen()
    {
        if (!optionsScreen.activeInHierarchy)
        {
            optionsScreen.SetActive(true);
            canAccessTutorials = false;
            playerRef.canMove = false;
            mainOptionButton.Select();
            Time.timeScale = 0f;
        }
        else
        {
            optionsScreen.SetActive(false);
            Time.timeScale = 1f;
            playerRef.canMove = true;
            canAccessTutorials = true;
        }
    }

    void WinScreen()
    {
        canAccessOptions = false;

        playerRef.canMove = false;

        winScreen.SetActive(true);
    }

    public void AccessSettings()
    {
        isSettingsOn = true;
    }

    public void CantAccessOptions()
    {
        canAccessOptions = false;
    }
}
