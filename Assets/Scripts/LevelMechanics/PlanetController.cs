using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !this.CompareTag("MainPlanet"))
        {
            Debug.Log("Nuevo Collider: " + this);
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (playerController.currentPlanet == transform) return;
                playerController.currentPlanet = transform;
                playerController.EnterNewGravityField();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("He salido del Trigger");
        if (other.CompareTag("Player") && !this.CompareTag("MainPlanet"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            playerController.currentPlanet = GameObject.FindWithTag("MainPlanet").transform;
            playerController.EnterNewGravityField();
        }
    }
}
