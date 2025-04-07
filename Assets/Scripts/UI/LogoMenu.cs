using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoMenu : MonoBehaviour
{
    public Animator fadeScreenAnim;
    public float logoScreenTime;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        LogoScreenAnimation();
    }

    void LogoScreenAnimation()
    {
        StartCoroutine(LogoScreenAnimationCO());
    }
    IEnumerator LogoScreenAnimationCO()
    {
        yield return new WaitForSeconds(logoScreenTime);
        fadeScreenAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
