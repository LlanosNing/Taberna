using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public int score, collectibles;
    public TextMeshProUGUI scoreText, collectibleText;

    public float lifeAmount = 100f;
    public Image lifeBar;

    public GameObject optionsScreen;



    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        UpdateLifeBar();

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            OptionsScreen();
        }
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;

        scoreText.text = score.ToString();
    }

    public void UpdateCollectibles(int addNumber)
    {
        collectibles += addNumber;

        collectibleText.text = score.ToString();
    }

    void UpdateLifeBar()
    {
        lifeAmount -= Time.deltaTime;

        lifeBar.fillAmount = lifeAmount / 100;
    }

    public void OptionsScreen()
    {
        if (!optionsScreen.activeInHierarchy)
        {
            optionsScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            optionsScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
