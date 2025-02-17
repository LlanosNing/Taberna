using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCow : MonoBehaviour
{
    CowController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<CowController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            controller.isMoving = false;
            controller.hitDirection = Vector3.zero;
        }
    }
}
