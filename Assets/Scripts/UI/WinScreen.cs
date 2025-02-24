using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public float winScreenTime;
    UIController uiRef;
    public Animator fadeScreenAnimator;
    // Start is called before the first frame update
    void Start()
    {
        uiRef = GetComponentInParent<UIController>();

        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(winScreenTime);

        fadeScreenAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("MainMenu");
    }
}
