using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm_Animation : MonoBehaviour
{
    float windEmission2 = 100, windEmission3 = 150;
    float dustEmission2 = 300, dustEmission3 = 400;
    float startSize2 = 10f, startSize3 = 15f;
    float positionZ2 = 3, positionZ3 = 5;
    float scaleZ2 = 10, scaleZ3 = 20;
    float windSpeed2 = 10f, windSpeed3 = 20f;
    float dustSpeed2 = 1f, dustSpeed3 = 4f;

    public float maxTimeDuration;
    float timeCounter;
    float lerpVelocity = 2.5f;
    public ParticleSystem windPS, dustPS;
    public bool animationOn;

    // Update is called once per frame
    void Update()
    {
        timeCounter -= Time.deltaTime;

        float elapsedTime = maxTimeDuration - timeCounter;
        float t = elapsedTime / maxTimeDuration;

        if (t >= 1f)
        {
            animationOn = false;
            return;
        }

        if(timeCounter > maxTimeDuration - 3)
        {
            var mainWind = windPS.main;
            var mainDust = dustPS.main;
            float newStartSize = Mathf.Lerp(0f, startSize2, t*lerpVelocity);
            mainWind.startSize = newStartSize;


            var emissionWind = windPS.emission;
            var emissionDust = dustPS.emission;
            float newWindRateOverTime = Mathf.Lerp(0f, windEmission2, t*lerpVelocity);
            emissionWind.rateOverTime = newWindRateOverTime;
            float newDustRateOverTime = Mathf.Lerp(0f, dustEmission2, t*lerpVelocity);
            emissionDust.rateOverTime = newDustRateOverTime;

            var shapeWind = windPS.shape;
            var shapeDust = dustPS.shape;
            shapeWind.position = Vector3.Lerp(shapeWind.position, new Vector3(shapeWind.position.x, shapeWind.position.y, positionZ2), t * lerpVelocity);
            shapeDust.position = Vector3.Lerp(shapeDust.position, new Vector3(shapeDust.position.x, shapeDust.position.y, positionZ2), t * lerpVelocity);
            shapeWind.scale = Vector3.Lerp(shapeWind.scale, new Vector3(shapeWind.scale.x, shapeWind.scale.y, scaleZ2), t * lerpVelocity);
            shapeDust.scale = Vector3.Lerp(shapeDust.scale, new Vector3(shapeDust.scale.x, shapeDust.scale.y, scaleZ2), t * lerpVelocity);

            var velocityWind = windPS.velocityOverLifetime;
            var velocityDust = dustPS.velocityOverLifetime;
            float newWindSpeed = Mathf.Lerp(0.5f, windSpeed2, t * lerpVelocity);
            velocityWind.speedModifier = newWindSpeed;
            float newDustSpeed = Mathf.Lerp(0.5f, dustSpeed2, t * lerpVelocity);
            velocityDust.speedModifier = newDustSpeed;
        }
        else if (timeCounter > maxTimeDuration - 6.75)
        {
            var mainWind = windPS.main;
            var mainDust = dustPS.main;
            float newStartSize = Mathf.Lerp(startSize2, startSize3, t * lerpVelocity);
            mainWind.startSize = newStartSize;


            var emissionWind = windPS.emission;
            var emissionDust = dustPS.emission;
            float newWindRateOverTime = Mathf.Lerp(windEmission2, windEmission3, t * lerpVelocity);
            emissionWind.rateOverTime = newWindRateOverTime;
            float newDustRateOverTime = Mathf.Lerp(dustEmission2, dustEmission3, t * lerpVelocity);
            emissionDust.rateOverTime = newDustRateOverTime;

            var shapeWind = windPS.shape;
            var shapeDust = dustPS.shape;
            shapeWind.position = Vector3.Lerp(shapeWind.position, new Vector3(shapeWind.position.x, shapeWind.position.y, positionZ3), t * lerpVelocity);
            shapeDust.position = Vector3.Lerp(shapeDust.position, new Vector3(shapeDust.position.x, shapeDust.position.y, positionZ3), t * lerpVelocity);
            shapeWind.scale = Vector3.Lerp(shapeWind.scale, new Vector3(shapeWind.scale.x, shapeWind.scale.y, scaleZ3), t * lerpVelocity);
            shapeDust.scale = Vector3.Lerp(shapeDust.scale, new Vector3(shapeDust.scale.x, shapeDust.scale.y, scaleZ3), t * lerpVelocity);

            var velocityWind = windPS.velocityOverLifetime;
            var velocityDust = dustPS.velocityOverLifetime;
            float newWindSpeed = Mathf.Lerp(windSpeed2, windSpeed3, t * lerpVelocity);
            velocityWind.speedModifier = newWindSpeed;
            float newDustSpeed = Mathf.Lerp(dustSpeed2, dustSpeed3, t * lerpVelocity);
            velocityDust.speedModifier = newDustSpeed;
        }
        else
        {
            var mainWind = windPS.main;
            var mainDust = dustPS.main;
            float newStartSize = Mathf.Lerp(startSize3, 1.5f, t * lerpVelocity);
            mainWind.startSize = newStartSize;


            var emissionWind = windPS.emission;
            var emissionDust = dustPS.emission;
            float newWindRateOverTime = Mathf.Lerp(windEmission3, 0f, t * lerpVelocity);
            emissionWind.rateOverTime = newWindRateOverTime;
            float newDustRateOverTime = Mathf.Lerp(dustEmission3, 0f, t * lerpVelocity);
            emissionDust.rateOverTime = newDustRateOverTime;

            var shapeWind = windPS.shape;
            var shapeDust = dustPS.shape;
            shapeWind.position = Vector3.Lerp(shapeWind.position, new Vector3(shapeWind.position.x, shapeWind.position.y, 0), t * lerpVelocity);
            shapeDust.position = Vector3.Lerp(shapeDust.position, new Vector3(shapeDust.position.x, shapeDust.position.y, 0), t * lerpVelocity);
            shapeWind.scale = Vector3.Lerp(shapeWind.scale, new Vector3(shapeWind.scale.x, shapeWind.scale.y, 5), t * lerpVelocity);
            shapeDust.scale = Vector3.Lerp(shapeDust.scale, new Vector3(shapeDust.scale.x, shapeDust.scale.y, 5), t * lerpVelocity);

            var velocityWind = windPS.velocityOverLifetime;
            var velocityDust = dustPS.velocityOverLifetime;
            float newWindSpeed = Mathf.Lerp(windSpeed3, 1f, t * lerpVelocity);
            velocityWind.speedModifier = newWindSpeed;
            float newDustSpeed = Mathf.Lerp(dustSpeed3, 1f, t * lerpVelocity);
            velocityDust.speedModifier = newDustSpeed;
        }
    }

    public void StartPartycleSystem()
    {
        var mainWind = windPS.main;
        var mainDust = dustPS.main;
        mainWind.startSize = 1.5f;
        

        var emissionWind = windPS.emission;
        var emissionDust = dustPS.emission;
        emissionWind.rateOverTime = 0;
        emissionDust.rateOverTime = 0;

        var shapeWind = windPS.shape;
        var shapeDust = dustPS.shape;
        shapeWind.position = new Vector3(0, 0, 0);
        shapeDust.position = new Vector3(0, 0, 0);
        shapeWind.scale = new Vector3(20, 40, 5);
        shapeDust.scale = new Vector3(20, 40, 5);

        var velocityWind = windPS.velocityOverLifetime;
        var velocityDust = dustPS.velocityOverLifetime;
        velocityWind.speedModifier = 3;
        velocityDust.speedModifier = 3;

        timeCounter = maxTimeDuration;

        windPS.Play();
        dustPS.Play();

        animationOn = true;
    }
}
