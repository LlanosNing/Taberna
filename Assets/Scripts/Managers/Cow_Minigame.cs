using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cow_Minigame : MonoBehaviour
{
    public int cowsRequired = 3;
    int cowsNum;

    public TextMeshProUGUI counterText;

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
