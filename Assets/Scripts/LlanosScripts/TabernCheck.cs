using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabernCheck : MonoBehaviour
{
    public GameObject letter;
    private PlayerInventory _playerI;

    private void Start()
    {
        letter.SetActive(false);
        _playerI = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _playerI.hasMeat == true)
        {
            letter.SetActive(true);
        }
    }
}
