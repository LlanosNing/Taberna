using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm_Animation : MonoBehaviour
{
    float windEmission2 = 50, windEmission3 = 150;
    float dustEmission2 = 300, dustEmission3 = 400;
    Vector2 startSize2 = new Vector2(1.5f, 3f), startSize3 = new Vector2(3f, 5f);
    float positionZ2 = 3, positionZ3 = 5;
    float scaleZ2 = 10, scaleZ3 = 20;
    float windSpeed2 = 1f, windSpeed3 = 10f;
    float dustSpeed2 = 1f, dustSpeed3 = 4f;

    public float maxTimeDuration;
    float timeCounter;

    public ParticleSystem windPS, dustPS;

    // Update is called once per frame
    void Update()
    {
        if(timeCounter > maxTimeDuration - 3)
        {
            var mainWind = windPS.main;
            var mainDust = dustPS.main;
        }
        else if (timeCounter > maxTimeDuration - 6.5)
        {
            var mainWind = windPS.main;
            var mainDust = dustPS.main;
        }
        else
        {
            var mainWind = windPS.main;
            var mainDust = dustPS.main;
        }
    }

    public void StartPartycleSystem()
    {
        timeCounter = maxTimeDuration;
        windPS.Play();
        dustPS.Play();
    }
}
