using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI fruits, days;
    public float fruitsNumber, daysNumber;

    void Update()
    {
        
    }
    public void SumarDias()
    {
        days.text = "" + daysNumber++;
    }
    public void SumarFrutas()
    {
        fruits.text = "" + fruitsNumber++;
    }
}
