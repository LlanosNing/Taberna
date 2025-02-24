using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private Inventory _playerInventory;
    public GameObject bombVisual;

    public bool canDestroy;
    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (canDestroy && Input.GetKeyDown(KeyCode.E))
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bombVisual.gameObject.SetActive(true);

            if (_playerInventory.hasBomb)
            {
                canDestroy = true;
            }
            else
            {
                Debug.Log("Necesitas una bomba!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bombVisual.gameObject.SetActive(false);

            canDestroy = false;
        }
    }
}
