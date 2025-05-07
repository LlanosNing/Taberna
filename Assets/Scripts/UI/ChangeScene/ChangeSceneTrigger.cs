using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator fadeScreenAnimator;
    public bool isPortalTwo, isPortalThree;
    public GameObject cantAccessMessage;
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Carrot_Planet"  && GameManager.endgame && sceneToLoad != "InteriorTaberna")
        {
            sceneToLoad = "InteriorTaberna";
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPortalTwo || isPortalThree)
        {
            cantAccessMessage.SetActive(false);
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCO());
    }

    IEnumerator LoadSceneCO()
    {
        Time.timeScale = 1f;

        if(sceneToLoad != "")
        {
            if (isPortalTwo)
            {
                if (GameManager.hasPortalTwoKey)
                {
                    fadeScreenAnimator.SetTrigger("FadeOut");
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    cantAccessMessage.SetActive(true);
                }
            }
            else if(isPortalThree)
            {
                if (GameManager.hasPortalThreeKey)
                {
                    fadeScreenAnimator.SetTrigger("FadeOut");
                    yield return new WaitForSeconds(1f);
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    cantAccessMessage.SetActive(true);
                }
            }
            else
            {
                fadeScreenAnimator.SetTrigger("FadeOut");
                if(SceneManager.GetActiveScene().name == "MainMenu")
                    yield return new WaitForSeconds(3f);
                else
                    yield return new WaitForSeconds(1f);
                SceneManager.LoadScene(sceneToLoad);
            }
            
        }
    }
}
