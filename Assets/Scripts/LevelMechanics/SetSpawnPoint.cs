using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPoint : MonoBehaviour
{
    private ThirdPersonController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.spawnPosition = other.transform.position;
            _playerController.spawnRotation = other.transform.rotation;
        }
    }
}
