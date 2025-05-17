using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameManager : MonoBehaviour
{
    public ParticleSystem newParticleSystem;
    public GameObject uIMessage;
    public TavernPlayerController playerController;
    public DialogActivator dialogActivator;
    public Animator fadeScreenAnim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndGameCO());
    }

    // Update is called once per frame
    void Update()
    {
        dialogActivator.canActivate = false;
    }

    IEnumerator EndGameCO()
    {
        newParticleSystem.Play();
        uIMessage.SetActive(true);
        playerController.canMove = false;
        yield return new WaitForSeconds(2.5f);
        fadeScreenAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Credits");
    }
}
