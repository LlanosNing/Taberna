using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float bounceForce = 200f;
    Animator animRef;

    public float maxTimeToHide;
    float timeToHideCounter;

    private void Start()
    {
        animRef = GetComponent<Animator>();
        timeToHideCounter = maxTimeToHide;
    }

    private void Update()
    {
        if (animRef.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            timeToHideCounter -= Time.deltaTime;

            if(timeToHideCounter <= 0)
            {
                animRef.SetTrigger("HitByPlayer");
                timeToHideCounter = maxTimeToHide;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animRef.SetTrigger("HitByPlayer");
            timeToHideCounter = maxTimeToHide;

            other.GetComponent<UltimatePlayerController>().Bounce(bounceForce);
        }
    }
}
