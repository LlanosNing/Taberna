using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public float defaultGravityForce = 1500f;
    public float gravityForce = 1500;

    public Vector3 GravityDirection
    {
        get
        {
            if (_gravityAreas.Count == 0) return Vector3.zero;
            _gravityAreas.Sort((area1, area2) => area1.Priority.CompareTo(area2.Priority));
            return _gravityAreas.Last().GetGravityDirection(this).normalized;
        }
    }

    private Rigidbody _rigidbody;
    private List<GravityArea> _gravityAreas;

    void Start()
    {
        gravityForce = defaultGravityForce;
        _rigidbody = transform.GetComponent<Rigidbody>();
        _gravityAreas = new List<GravityArea>();
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(GravityDirection * (gravityForce * Time.fixedDeltaTime), ForceMode.Acceleration);

        Quaternion upRotation = Quaternion.FromToRotation(transform.up, -GravityDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, upRotation * transform.rotation, Time.fixedDeltaTime * 3f); ;
        //Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, upRotation * _rigidbody.rotation, Time.fixedDeltaTime * 3f); ;
        //_rigidbody.MoveRotation(newRotation);
    }

    public void AddGravityArea(GravityArea gravityArea)
    {
        _gravityAreas.Add(gravityArea);
    }

    public void RemoveGravityArea(GravityArea gravityArea)
    {
        _gravityAreas.Remove(gravityArea);
    }
}
