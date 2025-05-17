using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teletransportePuertaArcana : MonoBehaviour
{
    public bool isOtherPlanet;
    public Transform destinyPoint;
    public Transform playerREf;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isOtherPlanet)
            {
                SceneManager.LoadScene("MarioPlaneta");
            }
            else
            {
                StartCoroutine(delayEnterPortal());
            }
        }
    }

    public IEnumerator delayEnterPortal()
    {
        yield return new WaitForSeconds(0.25f);
        playerREf.position = destinyPoint.position;
        playerREf.rotation = destinyPoint.rotation;
    }
}
