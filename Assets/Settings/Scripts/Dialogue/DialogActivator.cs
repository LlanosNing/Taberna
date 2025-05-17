using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    //Líneas del diálogo
    public string[] firstLines, repeatedLines;
    //Para saber si el diálogo se puede activar o no
    public bool canActivate;
    private bool hasBeenActivated;
    //Sprite de diálogo del NPC
    public Sprite [] npcSprites;

    private void Update()
    {
        if(canActivate && Input.GetButtonDown("Interact"))
        {
            if(!hasBeenActivated)
            {
                DialogManager.instance.ShowDialog(firstLines, npcSprites);
                hasBeenActivated = true;
            }
            else
            {
                DialogManager.instance.ShowDialog(repeatedLines, npcSprites);
            }
        }
    }

    //Si el jugador entra en la zona de Trigger puede activar el diálogo
    private void OnTriggerEnter(Collider collision)
    {
        //Llamamos al método que muestra el diálogo y le pasamos las líneas concretas que contiene este objeto
        if (collision.CompareTag("Player"))
        {
            canActivate = true;
            //GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity = Vector2.zero;
        }
    }

    //Si el jugador sale de la zona de Trigger ya no puede activar le diálogo
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
