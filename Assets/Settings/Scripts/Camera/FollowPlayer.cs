using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Range(.1f, 1f)]
    public float followDamping;
    public Transform playerTransform;
    public bool staticCamera;
    private void FixedUpdate()
    {
        if (!staticCamera)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, 1 / followDamping * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerTransform.rotation, 1 / followDamping * Time.fixedDeltaTime);
        }
        
    }
}
