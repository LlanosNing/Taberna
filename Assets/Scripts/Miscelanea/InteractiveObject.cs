using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    public Image papel;
    public Sprite spritePapel;
    public GameObject activarObjeto;
    public bool isRead;

    public UI uiRef;


    private void Start()
    {
        uiRef = GameObject.FindWithTag("UI").GetComponent<UI>();
    }

    public void ActivarObjeto()
    {
        
    }

    public void MostrarNota()
    {
        activarObjeto.SetActive(true);
        activarObjeto.GetComponent<Animator>().SetBool("isActive", true);

        if(isRead == false)
        {
            uiRef.UpdateCounter();

            isRead = true;
        }

        papel.sprite = spritePapel;
    }

    public void OcultarNota()
    {
       //activarObjeto.SetActive(false);
        activarObjeto.GetComponent<Animator>().SetBool("isActive", false);
    }
}
