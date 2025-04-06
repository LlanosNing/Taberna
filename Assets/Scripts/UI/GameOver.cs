using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public float fadeSpeed = 1.0f;
    //public Animator lifeBarAnim;
    //public Image gameOverPanel;

    public bool shouldFade, shouldUnfade;

    UIController uIRef;
    Respawn respawnRef;
    // Start is called before the first frame update
    void Start()
    {
        uIRef = GetComponent<UIController>();
        if(SceneManager.GetActiveScene().name == "MarioPlaneta")
            respawnRef = GameObject.FindWithTag("GameManager").GetComponent<Respawn>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (shouldFade)
        //{
        //    GameOverPanelFadeIn();

        //    //if(gameOverPanel.color.a >= 0.85f)
        //    //{
        //    //    shouldFade = false;
        //    //}
        //}

        //if (shouldUnfade)
        //{
        //    GameOverPanelFadeOut();

        //    //if (gameOverPanel.color.a <= 0f)
        //    //{
        //    //    shouldUnfade = false;
        //    //}
        //}
    }

    void GameOverPanelFadeIn()
    {
        //gameOverPanel.color = new Color(gameOverPanel.color.r, gameOverPanel.color.g, gameOverPanel.color.b, Mathf.MoveTowards(gameOverPanel.color.a, 0.85f, fadeSpeed * Time.deltaTime));
    }

    void GameOverPanelFadeOut()
    {
        //gameOverPanel.color = new Color(gameOverPanel.color.r, gameOverPanel.color.g, gameOverPanel.color.b, Mathf.MoveTowards(gameOverPanel.color.a, 0f, fadeSpeed * Time.deltaTime));
    }

    public void GameOverAnimation()
    {
        StartCoroutine(GameOverCO());
    }

    private IEnumerator GameOverCO()
    {
        shouldFade = true;

        //lifeBarAnim.SetTrigger("ScaleUp");

        yield return new WaitForSeconds(1f);

        //uIRef.LifeBarLossAnimation(20f);

        yield return new WaitForSeconds(1f);

        //lifeBarAnim.SetTrigger("ScaleDown");

        yield return new WaitForSeconds(0.5f);

        shouldUnfade = true;

        yield return new WaitForSeconds(0.5f);

        //respawnRef.RespawnPlayer();
    }
}
