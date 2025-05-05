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

    public Animator pickAnim;

    bool isPicked;

    private void Start()
    {
        pickAnim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPicked)
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

            pickAnim.SetTrigger("Pickup");
            isPicked = true;
        }
    }
}
