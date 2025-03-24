using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantCarrot : MonoBehaviour
{
    public int maxSink;
    int sinkLevels;
    public float sinkDistance;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && sinkLevels < maxSink)
        {
            if(collision.gameObject.GetComponent<UltimatePlayerController>().canGroundPoundCarrot)
            {
                sinkLevels++;

                transform.position -= new Vector3(0, sinkDistance, 0);
            }
        }
    }
}
