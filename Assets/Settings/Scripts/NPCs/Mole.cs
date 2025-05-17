using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public Animator animRef;

    public float minTimeToAppear, maxTimeToAppear;
    float timeToAppearCounter;
    public float maxTimeToHide;
    float timeToHideCounter;


    private void Start()
    {
        animRef = GetComponentInChildren<Animator>();
        timeToHideCounter = maxTimeToHide;
    }

    private void Update()
    {
        if (animRef.GetCurrentAnimatorStateInfo(0).IsName("Underground") && timeToAppearCounter <= 0)
        {
            timeToAppearCounter = Random.Range(minTimeToAppear, maxTimeToAppear);
            Debug.Log("El topo " + gameObject.name + " tardará " +  timeToAppearCounter + " segundos en aparecer");
        }

        if(timeToAppearCounter > 0)
        {
            timeToAppearCounter -= Time.deltaTime;
        }
        if(timeToAppearCounter <= 0)
        {
            animRef.SetTrigger("Jump");
        }
    }
}
