using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject[] planetas;
    public float velocidad = 5f;
    public float velRotacion = 200;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //la gravedad sale desde el planeta seleccionado
        Physics.gravity = planetas[0].transform.position - transform.position;
        //alinear el eje vertical del player con el planeta
        transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;
    }
}
