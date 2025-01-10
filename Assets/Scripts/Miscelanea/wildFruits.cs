using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wildFruits : MonoBehaviour
{
    public void ObtenerFruta()
    {
        gameObject.SetActive(false);
        Debug.Log("e");
        GetComponentInParent<NaboCrece>().Reiniciar();
    }
}
