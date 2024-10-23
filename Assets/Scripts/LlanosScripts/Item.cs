using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject item;
    public Collider cll;
    public bool hasItem = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(delayDestroy());
            hasItem = true;
        }
    }
    public IEnumerator delayDestroy()
    {
        cll.enabled = false;
        yield return new WaitForSeconds(0.7f);
        Destroy(item);
    }
}
