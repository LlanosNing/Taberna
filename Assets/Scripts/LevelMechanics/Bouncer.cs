using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private ThirdPersonController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.Bounce();
        }
    }
}
