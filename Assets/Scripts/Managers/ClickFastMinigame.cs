using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFastMinigame : MonoBehaviour
{
    public float minigameDuration;
    public float timeCounter;

    public float clicks;
    public float timePassed;
    public float cps;
    public float cpsThreshold;
    // Start is called before the first frame update
    void Start()
    {
        timeCounter = minigameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCPS();

        if (timeCounter <= 0)
            EndMinigame();
    }

    void CalculateCPS()
    {
        if (Input.GetButtonDown("Fire1"))
            clicks++;
        timePassed += Time.deltaTime;
        cps = clicks / timePassed;

        if(cps > cpsThreshold )
            timeCounter -= Time.deltaTime;

        if(timePassed > 2f)
        {
            clicks = 0;
            timePassed = 0;
        } 
    }

    void EndMinigame()
    {

    }
}
