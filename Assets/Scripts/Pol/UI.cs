using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public int notes;
    public TextMeshProUGUI counterNotesRef;

    void Start()
    {
        notes--;
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        notes++;
        counterNotesRef.text = notes.ToString();
    }
    

    


}
