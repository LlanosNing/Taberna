using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private Inventory _playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerInventory.hasBomb)
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Necesitas una bomba!");
            }
        }
    }
}