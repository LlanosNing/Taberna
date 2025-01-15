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
    float maxLifeAmount;

    public float lifeLossAmount;
    public float lifeLossSpeed = 5f;

    public Image lifeBar;

    public GameObject optionsScreen;
    public bool canAccessOptions;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();

        maxLifeAmount = lifeAmount;
    }

    private void Update()
    {
        UpdateLifeBar();

        if(lifeLossAmount > 0)
        {
            lifeLossAmount -= lifeLossSpeed * Time.deltaTime;
            lifeAmount -= lifeLossSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canAccessOptions) 
        {
            OptionsScreen();
        }

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

    void UpdateLifeBar()
    {
        lifeAmount -= Time.deltaTime;

        lifeBar.fillAmount = lifeAmount / maxLifeAmount;
    }

    public void LifeBarLossAnimation(float amount)
    {
        lifeLossAmount = amount;
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
