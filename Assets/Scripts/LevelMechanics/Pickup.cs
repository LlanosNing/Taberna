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
    Animator animRef;

    private void Start()
    {
        animRef = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(canPick && Input.GetButtonDown("Interact"))
        {
            if (isPortalTwoKey)
            {
                GameManager.hasPortalTwoKey = true;
                animRef.SetTrigger("Pickup");
            }

            if (isPortalThreeKey)
            {
                GameManager.hasPortalThreeKey = true;
                animRef.SetTrigger("Pickup");
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
            //gameObject.SetActive(false);
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        uiInteractMessage.SetActive(true);
        canPick = true;           
    }

    public void OnTriggerExit(Collider other)
    {
        uiInteractMessage.SetActive(false);
        canPick = false;
    }
}
