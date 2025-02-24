using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOutside : MonoBehaviour
{
    public GameObject teleportPoint;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = teleportPoint.transform.position;
            transform.rotation = teleportPoint.transform.localRotation;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
