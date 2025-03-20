using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isPortalTwoKey;
    public bool isPortalThreeKey;
    public bool showsOnMinimap;

    public int coinValue;

    private UIController _uIRef;
    public GameObject minimapRepresentation;

    //private Inventory _playerInventory;
    //public bool isCoin;
    //public bool isBomb;
    //public bool isCollectible;

    private void Start()
    {
        _uIRef = GameObject.FindWithTag("Canvas").GetComponent<UIController>();

        //_playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isPortalTwoKey)
            {
                GameManager.hasPortalTwoKey = true;
            }

            if (isPortalThreeKey)
            {
                GameManager.hasPortalThreeKey = true;
            }

            Destroy(gameObject);

            if (showsOnMinimap)
            {
                minimapRepresentation.SetActive(false);
            }

            //if(isCoin)
            //{
            //    _uIRef.UpdateScore(coinValue);
            //}

            //if(isBomb)
            //{
            //    _playerInventory.hasBomb = true;
            //}

            //if(isCollectible)
            //{
            //    _uIRef.UpdateCollectibles();
            //}

        }
    }
}
