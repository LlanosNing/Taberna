using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory _playerInventory;

    [Header ("PICKUP CLASS")]
    public bool isCoin;
    public bool isBomb;
    public bool isCollectible;
    public bool showsOnMinimap;

    public int coinValue;

    private UIController _uIRef;
    public GameObject minimapRepresentation;

    private void Start()
    {
        _playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();

        _uIRef = GameObject.FindWithTag("Canvas").GetComponent<UIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isCoin)
            {
                _uIRef.UpdateScore(coinValue);
            }

            if(isBomb)
            {
                _playerInventory.hasBomb = true;
            }

            if(isCollectible)
            {
                _uIRef.UpdateCollectibles();
            }

            Destroy(gameObject);

            if (showsOnMinimap)
            {
                minimapRepresentation.SetActive(false);
            }
        }
    }
}
