using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    public Image papel;
    public Sprite spritePapel;
    public GameObject activarObjeto;

    

    private void Start()
    {
        
    }

    public void ActivarObjeto()
    {
        
    }

    public void MostrarNota()
    {
        activarObjeto.SetActive(true);
        activarObjeto.GetComponent<Animator>().SetBool("isActive", true);

        papel.sprite = spritePapel;
    }

    public void OcultarNota()
    {
       //activarObjeto.SetActive(false);
        activarObjeto.GetComponent<Animator>().SetBool("isActive", false);
    }
}
