using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory _playerInventory;

    [Header ("PICKUP CLASS")]
    public bool isCoin;
    public bool isBomb;

    private void Start()
    {
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isCoin)
            {
                Destroy(gameObject);
            }

            if(isBomb)
            {
                _playerInventory.hasBomb = true;
                Destroy(gameObject);
            }
        }
    }
}
