using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabernCheck : MonoBehaviour
{
    public GameObject letter;
    private Item itemRef;

    private void Start()
    {
        letter.SetActive(false);
        itemRef = GameObject.Find("carne").GetComponent<Item>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && itemRef.hasItem == true)
        {
            letter.SetActive(true);
        }
    }
}
