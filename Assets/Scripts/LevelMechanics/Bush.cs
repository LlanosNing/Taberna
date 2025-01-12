using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public GameObject peppers;
    int pickCounter = 0;
    public int maxPickCounter = 5;

    public bool canPick;
    public bool isPicked;

    public int coinValue = 50;
    UIController uiRef;

    private void Start()
    {
        uiRef = GameObject.FindWithTag("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canPick && Input.GetKeyDown(KeyCode.E))
        {
            pickCounter++;

            if (pickCounter >= maxPickCounter)
            {
                peppers.SetActive(false);
                isPicked = true;
                uiRef.UpdateScore(coinValue);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPicked)
            {
                canPick = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPick = false;
            pickCounter = 0;
        }
    }
}
