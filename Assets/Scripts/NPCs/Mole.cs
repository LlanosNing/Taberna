using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public Animator animRef;

    public float minTimeToAppear, maxTimeToAppear;
    float timeToAppearCounter;
    public bool hasToAppear;
    public float maxTimeToHide;
    float timeToHideCounter;

    private void Start()
    {
        animRef = GetComponentInChildren<Animator>();
        timeToHideCounter = maxTimeToHide;
    }

    private void Update()
    {
        if (animRef.GetCurrentAnimatorStateInfo(0).IsName("Underground") && !hasToAppear && timeToAppearCounter <= 0)
        {
            timeToAppearCounter = Random.Range(minTimeToAppear, maxTimeToAppear);
            Debug.Log("El topo " + gameObject.name + " tardará " +  timeToAppearCounter + " segundos en aparecer");
            hasToAppear = true;
        }

        if(timeToAppearCounter > 0)
        {
            timeToAppearCounter -= Time.deltaTime;
        }
        if(timeToAppearCounter <= 0 && hasToAppear)
        {
            animRef.SetTrigger("Jump");
        }

        if (animRef.GetCurrentAnimatorStateInfo(0).IsName("Underground"))
        {
            timeToHideCounter -= Time.deltaTime;

            if(timeToHideCounter <= 0)
            {
                timeToHideCounter = maxTimeToHide;
                hasToAppear = false;
            }
        }
    }
}
