using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindPush : MonoBehaviour
{
    public float windForce = 20f;

    public float windInterval = 5f;
    private float windIntervalCounter;
    public float windDuration = 1.5f;
    public float windDurationCounter;

    public bool canGrip;
    public bool windEnabled;

    private Rigidbody rb;
    private Transform planet;
    public Vector3 currentWindDirection; // Dirección del viento actual
    UltimatePlayerController pController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindWithTag("MainPlanet").transform;
        pController = GetComponent<UltimatePlayerController>();

        windIntervalCounter = windInterval;
    }

    private void Update()
    {
        if(canGrip && Input.GetButton("Interact"))
        {
            windEnabled = false;
            pController.canMove = false;
            rb.velocity = Vector3.zero;
        }

        if (Input.GetButtonUp("Interact"))
        {
            windEnabled = true;
            pController.canMove = true;
        }
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
            if (windEnabled)
            {
                ApplyWindForce();
            }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GripZone"))
        {
            canGrip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GripZone"))
        {
            canGrip = false;
        }
    }
}