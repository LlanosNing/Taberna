using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cow_Minigame : MonoBehaviour
{
    public int cowsRequired, faseTwoThreshold, faseThreeThreshold, faseFourThreshold;
    int cowsNum;
    public float faseFourMinTime, faseFourMaxTime;
    bool faseTwo, faseThree, faseFour;

    public TextMeshProUGUI counterText;
    public GameObject[] molesFaseTwo, molesFaseThree;
    public GameObject lastCow;
    public GameObject cowsParent;

    public Animator fadeScreenAnim;
    public Transform minigameStartPosition;
    public Vector3 minigameEndPosition;

    public ParticleSystem playerQuestVFX;

    public GameObject interactCowMessage;

    private void Start()
    {
        StartCoroutine(StartTransitionCO());
    }

    private void Update()
    {
        if(cowsNum == faseTwoThreshold && !faseTwo)
        {
            foreach (GameObject mole in molesFaseTwo)
            {
                mole.SetActive(true);
            }

            faseTwo = true;
        }
        
        if(cowsNum == faseThreeThreshold && !faseThree)
        {
            foreach (GameObject mole in molesFaseTwo)
            {
                mole.SetActive(false);
            }

            foreach (GameObject mole in molesFaseThree)
            {
                mole.SetActive(true);
            }

            faseThree = true;
        }

        if(cowsNum >= faseFourThreshold && !faseFour)
        {
            foreach (GameObject mole in molesFaseTwo)
            {
                mole.SetActive(true);
                mole.GetComponent<Mole>().minTimeToAppear = faseFourMinTime;
                mole.GetComponent<Mole>().maxTimeToAppear = faseFourMaxTime;
                mole.GetComponent<Mole>().animRef.Play("Underground", -1, 0f);
            }
            foreach (GameObject mole in molesFaseThree)
            {
                mole.SetActive(true);
                mole.GetComponent<Mole>().minTimeToAppear = faseFourMinTime;
                mole.GetComponent<Mole>().maxTimeToAppear = faseFourMaxTime;
                mole.GetComponent<Mole>().animRef.Play("Underground", -1, 0f);
            }

            lastCow.SetActive(true);
            faseFour = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cow"))
        {
            AudioManager.aMRef.PlaySFX(9);
            UpdateCowCounter();
            other.gameObject.SetActive(false);
        }
    }

    void UpdateCowCounter()
    {
        cowsNum++;
        Debug.Log(cowsNum + " vaca/s devueltas");
        counterText.text = cowsNum.ToString();
        if (cowsNum >= cowsRequired)
        {
            StartCoroutine(EndTransitionCO());
        }
    }

    IEnumerator StartTransitionCO()
    {
        fadeScreenAnim.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1);

        minigameEndPosition = GameObject.FindWithTag("Player").transform.position;
        GameObject.FindWithTag("Player").transform.position = minigameStartPosition.position;

        fadeScreenAnim.SetTrigger("FadeIn");

        cowsParent.SetActive(true);

        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().NextQuest();
    }

    IEnumerator EndTransitionCO()
    {
        GameObject.FindWithTag("QuestCompleted").GetComponent<Animator>().SetTrigger("QuestCompleted");

        playerQuestVFX.Play();

        AudioManager.aMRef.PlaySFX(6);

        if (interactCowMessage.activeInHierarchy)
        {
            interactCowMessage.SetActive(false);
        }

        yield return new WaitForSeconds(2);

        fadeScreenAnim.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1);

        GameObject.FindWithTag("Player").transform.position = minigameEndPosition;

        fadeScreenAnim.SetTrigger("FadeIn");

        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().NextQuest();

        if (GameManager.hasPortalTwoKey)
        {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().NextQuest();
        }
    }
}
