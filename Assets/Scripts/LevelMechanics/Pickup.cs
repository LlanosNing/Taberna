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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isPortalTwoKey)
            {
                GameManager.hasPortalTwoKey = true;
            }

            if (isPortalThreeKey)
            {
                GameManager.hasPortalThreeKey = true;
            }

            if (isLizard)
            {
                GameObject.FindWithTag("DesertMinigame").GetComponent<Desert_Minigame>().AddLizardTail();
            }

            if (showsOnMinimap)
            {
                minimapRepresentation.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}
