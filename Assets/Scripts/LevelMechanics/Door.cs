using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject otherDoor;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.transform.position = otherDoor.transform.position;
            _player.transform.rotation = otherDoor.transform.rotation;
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
