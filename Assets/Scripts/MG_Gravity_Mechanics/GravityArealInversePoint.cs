using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityArealInversePoint : GravityArea
{
    [SerializeField] private Vector3 _center;


    public override Vector3 GetGravityDirection(GravityBody _gravityBody)
    {
        return (_gravityBody.transform.position - _center).normalized;
    }
}
