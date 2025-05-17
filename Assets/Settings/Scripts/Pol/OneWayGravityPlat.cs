using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayGravityPlat : MonoBehaviour
{
    public MeshCollider collRef;
    void Start()
    {
        collRef = GetComponentInParent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        collRef.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            collRef.enabled = false;
    }
}
