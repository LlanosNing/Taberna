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
    TavernPlayerController tavernPlayerRef;
    public bool canAccessOptions;

    public bool canAccessTutorials;

    public Button mainOptionButton;
    public bool isSettingsOn;

    CameraArmController armController;

    public bool tavernScene;

    private void Start()
    {

        score = 0;
        if(scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        //maxLifeAmount = lifeAmount;
        if (tavernScene)
        {
            tavernPlayerRef = GameObject.FindWithTag("Player").GetComponent<TavernPlayerController>();
        }
        else
        {
            playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();

            armController = GameObject.FindWithTag("CameraPivot").GetComponent<CameraArmController>();
        }

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

        if (Input.GetButtonDown("Cancel") && canAccessOptions) 
        {
            if(isSettingsOn)
            {
                isSettingsOn = false;
                mainOptionButton.Select();
                if(!tavernScene)
                    armController.staticCamera = false;
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

            if (tavernScene)
            {
                tavernPlayerRef.canMove = false;
            }
            else 
            {
                playerRef.canMove = false;
                armController.staticCamera = true;
            }
            
            mainOptionButton.Select();
            Time.timeScale = 0f;
        }
        else
        {
            optionsScreen.SetActive(false);
            Time.timeScale = 1f;
            if (tavernScene)
            {
                tavernPlayerRef.canMove = true;
            }
            else
            {
                playerRef.canMove = true;
                armController.staticCamera = false;
            }
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
