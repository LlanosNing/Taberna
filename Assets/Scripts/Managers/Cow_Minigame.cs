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
            }
            foreach (GameObject mole in molesFaseThree)
            {
                mole.SetActive(true);
                mole.GetComponent<Mole>().minTimeToAppear = faseFourMinTime;
                mole.GetComponent<Mole>().maxTimeToAppear = faseFourMaxTime;
            }

            lastCow.SetActive(true);
            faseFour = true;
        }

        if(cowsNum >= cowsRequired)
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cow"))
        {
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
            //Cambiar de escena
        }
    }
}
