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
    public static bool endgame;
    public GameObject endgameManager;

    public static string[] quests = 
        { 
        "Consigue Leche", 
        "Reúne las vacas en el granero",
        "Encuentra la Piedra del segundo Portal",
        "Consigue Colas de Lagarto",
        "Encuentra la Piedra del tercer Portal",
        "Consigue Raíces de Zanahoria Fosilizada",
        "Vuelve a la taberna"
        };
    public static int questIndex;

    public TextMeshProUGUI questText;
    public Animator questBoxAnim;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            if(!hasPortalTwoKey)
                GameObject.FindWithTag("Portal2").SetActive(false);

            if (!hasPortalThreeKey)
                GameObject.FindWithTag("Portal3").SetActive(false);
        }

        if(SceneManager.GetActiveScene().name == "InteriorTaberna" && endgame)
        {
            GameObject.FindWithTag("DialogueManager").GetComponent<DialogManager>().managerToActivate = endgameManager;
        }

        if(SceneManager.GetActiveScene().name != "InteriorTaberna")
            questText.text = quests[questIndex];
    }
    public void NextQuest()
    {
        questIndex++;

        questText.text = quests[questIndex];

        questBoxAnim.SetTrigger("Reveal");
    }
}
