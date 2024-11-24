using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject item;
    public Collider cll;
    private PlayerInventory _playerI;
    public bool isWater;
    public GameObject waterPanel;

    private void Start()
    {
        _playerI = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(delayDestroy());
            if(isWater == true)
            {
                waterPanel.SetActive(true);
            }
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
