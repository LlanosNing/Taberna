using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Desert_Minigame : MonoBehaviour
{
    public int lizardTails;
    public TextMeshProUGUI uiCounterText;
    public GameObject lizardQuest, portalStoneQuest;

    public void AddLizardTail()
    {
        lizardTails++;
        uiCounterText.text = lizardTails.ToString();

        if (lizardTails >= 3)
        {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().NextQuest();
            
            if(GameManager.hasPortalThreeKey)
            {
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().NextQuest();
            }
        }
    }
}
