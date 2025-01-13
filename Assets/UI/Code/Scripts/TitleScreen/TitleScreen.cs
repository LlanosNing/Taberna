using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public Animator pABAnim, titleAnim, fadeAnim;
    public AudioSource selectAudio;
    private ChangeSceneScript _changeSceneScript;

    public Transform cameraWaypoint;
    private CameraTraveling _cameraTravelingScript;

    private void Start()
    {
        _cameraTravelingScript = Camera.main.GetComponent<CameraTraveling>();

        _changeSceneScript = GetComponent<ChangeSceneScript>();
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

                _changeSceneScript.ChangeScene();

                selectAudio.Play();
            }
        }
    }
}
