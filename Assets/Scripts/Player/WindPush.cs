using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPush : MonoBehaviour
{
    public float windForce = 20f;

    public float windInterval = 5f;
    private float windIntervalCounter;
    public float windDuration = 1.5f;
    private float windDurationCounter;

    private Rigidbody rb;
    private Transform planet;
    public Vector3 currentWindDirection; // Dirección del viento actual

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindWithTag("MainPlanet").transform;

        windIntervalCounter = windInterval;
    }

    private void FixedUpdate()
    {
        if (windIntervalCounter > 0)
        {
            windIntervalCounter -= Time.deltaTime;

            if (windIntervalCounter <= 0)
            {
                windDurationCounter = windDuration;
                SetWindDirection(); // Solo calculamos la dirección aquí
            }
        }
        else if (windDurationCounter > 0)
        {
            ApplyWindForce(); // Mantener la misma dirección de viento

            windDurationCounter -= Time.deltaTime;

            if (windDurationCounter <= 0)
            {
                windIntervalCounter = windInterval;
            }
        }
    }

    void SetWindDirection()
    {
        // Obtener vector de dirección desde el planeta al jugador (radial)
        Vector3 radialDirection = (rb.position - planet.position).normalized;

        // Calcular una dirección tangencial aleatoria
        currentWindDirection = Vector3.Cross(radialDirection, Random.onUnitSphere).normalized;

        // Aleatorizar la inversión de la dirección del viento
        if (Random.value > 0.5f) // 50% de probabilidad para invertir la dirección
        {
            currentWindDirection = -currentWindDirection;
        }
    }

    void ApplyWindForce()
    {
        // Aplicar la fuerza en la dirección previamente calculada
        rb.AddForce(currentWindDirection * windForce * Time.deltaTime, ForceMode.Acceleration);
    }
}