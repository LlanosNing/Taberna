using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCharTest : MonoBehaviour
{
    public GameObject[] planetas;
    public Vector3 gravityDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gravityDirection = planetas[0].transform.position - transform.position;

        Physics.gravity = gravityDirection;

        transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;
    }
}
