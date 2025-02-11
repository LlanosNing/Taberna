using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wildFruits : MonoBehaviour
{
    public UiManager uiManager;

    public void ObtenerFruta()
    {
        uiManager.SumarFrutas();
        StartCoroutine(delayFruits());
        gameObject.SetActive(false);
        Debug.Log("e");
        GetComponentInParent<NaboCrece>().Reiniciar();
    }

    private IEnumerator delayFruits()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
