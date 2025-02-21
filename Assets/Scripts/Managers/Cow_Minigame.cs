using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cow_Minigame : MonoBehaviour
{
    public int cowsRequired, faseTwoThreshold, faseThreeThreshold;
    int cowsNum;
    bool faseTwo, faseThree;

    public TextMeshProUGUI counterText;
    public GameObject[] molesFaseTwo, molesFaseThree;

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
            foreach (GameObject mole in molesFaseThree)
            {
                mole.SetActive(true);
            }

            faseThree = true;
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
