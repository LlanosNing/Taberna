using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teletransportePuertaArcana : MonoBehaviour
{
    public Transform destinyPoint;
    public Transform playerREf;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(delayEnterPortal());
        }
    }

    public IEnumerator delayEnterPortal()
    {
        yield return new WaitForSeconds(0.25f);
        playerREf.position = destinyPoint.position;
        playerREf.rotation = destinyPoint.rotation;
    }
}
