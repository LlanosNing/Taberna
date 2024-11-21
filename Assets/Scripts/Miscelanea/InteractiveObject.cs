using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public int[] notas;

    public void ActivarObjeto()
    {
        Destroy(gameObject);
    }
}
