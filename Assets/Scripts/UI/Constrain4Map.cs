using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Este script se asignará en los objetos que sigan a otros en el minimapa, como la cámara Minimapa con el jugador)
public class Constrain4Map : MonoBehaviour
{
    public Transform target;
    public Transform map_obj; //Plano Minimapa para saber a qué altura está
    public bool rotationContrain = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationContrain == true)
        {
            transform.rotation = target.rotation;

        }
    }
}
