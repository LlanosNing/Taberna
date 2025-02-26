using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float bounceForce = 200f;
    Animator animRef;

    public float minTimeToAppear, maxTimeToAppear;
    float timeToAppearCounter;
    public bool hasToAppear;
    public float maxTimeToHide;
    float timeToHideCounter;

    private void Start()
    {
        animRef = GetComponent<Animator>();
        timeToHideCounter = maxTimeToHide;
    }

    private void Update()
    {
        if (animRef.GetCurrentAnimatorStateInfo(0).IsName("UnderGround") && !hasToAppear && timeToAppearCounter <= 0)
        {
            timeToAppearCounter = Random.Range(minTimeToAppear, maxTimeToAppear);
            Debug.Log("El topo " + gameObject.name + " tardará" +  timeToAppearCounter + " segundos en aparecer");
            hasToAppear = true;
        }

        if(timeToAppearCounter > 0)
        {
            timeToAppearCounter -= Time.deltaTime;
        }
        if(timeToAppearCounter <= 0 && hasToAppear)
        {
            animRef.SetBool("HasToAppear", true);
        }

        if (animRef.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            timeToHideCounter -= Time.deltaTime;

            if(timeToHideCounter <= 0)
            {
                animRef.SetTrigger("HitByPlayer");
                timeToHideCounter = maxTimeToHide;
                hasToAppear = false;
                animRef.SetBool("HasToAppear", false);
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
