using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Este script se asignará en los objetos que sigan a otros en el minimapa, como la cámara Minimapa con el jugador)
public class Constrain4Map : MonoBehaviour
{
    public Transform target;
    public Transform map_obj; //Plano Minimapa para saber a qué altura está
    public bool positionConstrain = true;
    public bool rotationContrain = true;
    public float offsetY = 0.1f; //Para que no se solapen los elementos

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(positionConstrain == true)
        {
            //transform.position = new Vector3(target.position.x, map_obj.position.y + offsetY, target.position.z);
        }
        if (rotationContrain == true)
        {
            transform.rotation = target.rotation;

        }



    }
}
