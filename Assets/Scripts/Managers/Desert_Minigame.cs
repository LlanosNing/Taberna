using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Desert_Minigame : MonoBehaviour
{
    public int lizardTails;
    public TextMeshProUGUI uiCounterText;

    public void AddLizardTail()
    {
        lizardTails++;
        uiCounterText.text = lizardTails.ToString();
    }
}
