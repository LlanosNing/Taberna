using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool hasPortalTwoKey;
    public static bool hasPortalThreeKey;
    public static int lastPortal;
    public static int questNumber;
    public static int joaquinDialog;

    public static string[] quests = 
        { 
        "Consigue Leche", 
        "Re�ne las vacas en el granero",
        "Encuentra la Piedra del segundo Portal",
        "Consigue Colas de Lagarto",
        "Encuentra la Piedra del tercer Portal",
        "Consigue Ra�ces de Zanahoria Fosilizada",
        "Habla con Joaqu�n"
        };
    public static int questIndex;

    public TextMeshProUGUI questText;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Lobby" && !hasPortalTwoKey)
        {
            GameObject.FindWithTag("Portal2").SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Lobby" && !hasPortalThreeKey)
        {
            GameObject.FindWithTag("Portal3").SetActive(false);
        }

        questText.text = quests[questIndex];
    }
    public void NextQuest()
    {
        questIndex++;

        questText.text = quests[questIndex];
    }
}
