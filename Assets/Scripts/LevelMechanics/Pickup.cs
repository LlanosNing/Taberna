using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isPortalTwoKey;
    public bool isPortalThreeKey;
    public bool isLizard;
    public bool showsOnMinimap;

    public GameObject minimapRepresentation;

    public GameObject uiInteractMessage;
    public bool canPick;

    public Animator pickAnim;

    bool isPicked;

    private void Start()
    {
        pickAnim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(canPick && Input.GetButtonDown("Interact") && !isPicked)
        {
            if (isPortalTwoKey)
            {
                GameManager.hasPortalTwoKey = true;

                if (GameManager.questIndex == 2)
                {
                    GameObject.FindWithTag("GameManager").GetComponent<GameManager>().NextQuest();
                }
            }

            if (isPortalThreeKey)
            {
                GameManager.hasPortalThreeKey = true;

                if (GameManager.questIndex == 4)
                {
                    GameObject.FindWithTag("GameManager").GetComponent<GameManager>().NextQuest();
                }
            }

            if (isLizard)
            {
                GameObject.FindWithTag("DesertMinigame").GetComponent<Desert_Minigame>().AddLizardTail();
            }

            if (showsOnMinimap)
            {
                minimapRepresentation.SetActive(false);
            }

            uiInteractMessage.SetActive(false);
            pickAnim.SetTrigger("Pickup");
            isPicked = true;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPicked)
        {
            uiInteractMessage.SetActive(true);
            canPick = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        uiInteractMessage.SetActive(false);
        canPick = false;
    }
}
