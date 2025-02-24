using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTraveling : MonoBehaviour
{
    public Transform target;
    public float travelingSpeed = 2f;
    public bool canChangeTarget;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, travelingSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, travelingSpeed * Time.deltaTime);
    }

    public void ChangeTarget(Transform newtarget)
    {
        if(canChangeTarget)
        {
            target = newtarget;
        }
    }
}
