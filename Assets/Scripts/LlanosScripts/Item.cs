using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject item;
    public Collider cll;
    private PlayerInventory _playerI;

    private void Start()
    {
        _playerI = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(delayDestroy());
            _playerI.hasMeat = true;
        }
    }
    public IEnumerator delayDestroy()
    {
        cll.enabled = false;
        yield return new WaitForSeconds(0.7f);
        Destroy(item);
    }
}
