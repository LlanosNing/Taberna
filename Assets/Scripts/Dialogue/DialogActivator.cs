using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    //L�neas del di�logo
    public string[] firstLines, repeatedLines;
    //Para saber si el di�logo se puede activar o no
    private bool canActivate;
    private bool hasBeenActivated;
    //Sprite de di�logo del NPC
    public Sprite theNpcSprite;

    private void Update()
    {
        if(canActivate && Input.GetButtonDown("Interact"))
        {
            if(!hasBeenActivated)
            {
                DialogManager.instance.ShowDialog(firstLines, theNpcSprite);
                hasBeenActivated = true;
            }
            else
            {
                DialogManager.instance.ShowDialog(repeatedLines, theNpcSprite);
            }
        }
    }

    //Si el jugador entra en la zona de Trigger puede activar el di�logo
    private void OnTriggerEnter(Collider collision)
    {
        //Llamamos al m�todo que muestra el di�logo y le pasamos las l�neas concretas que contiene este objeto
        if (collision.CompareTag("Player"))
        {
            canActivate = true;
            //GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity = Vector2.zero;
        }
    }

    //Si el jugador sale de la zona de Trigger ya no puede activar le di�logo
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
