using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class wildFruits : MonoBehaviour
{
    public GameObject fruit;

    public void ObtenerFruta()
    {
        fruit.SetActive(false);
    }
}
