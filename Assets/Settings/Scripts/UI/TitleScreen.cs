using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string sceneToLoad;
    public float transitionDuration;
    public Animator pABAnim, titleAnim, fadeAnim;
    public AudioSource selectAudio;

    public Transform cameraWaypoint;
    private CameraTraveling _cameraTravelingScript;

    private void Start()
    {
        _cameraTravelingScript = Camera.main.GetComponent<CameraTraveling>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pABAnim != null && titleAnim != null && fadeAnim != null) 
        {
            if (Input.anyKeyDown)
            {
                pABAnim.SetTrigger("GoOut");
                titleAnim.SetTrigger("GoOut");
                fadeAnim.SetTrigger("FadeOut");

                _cameraTravelingScript.target = cameraWaypoint;

                NextScene();

                selectAudio.Play();
            }
        }
    }

    void NextScene()
    {
        StartCoroutine(NextSceneCO());
    }

    IEnumerator NextSceneCO()
    {
        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(sceneToLoad);
    }
}
