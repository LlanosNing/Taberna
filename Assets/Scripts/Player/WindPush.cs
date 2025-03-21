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
    public bool windEnabled = true;

    private Rigidbody rb;
    private Transform planet;
    private Vector3 baseWindDirection; // Direcci�n base del viento (sin ajuste por rotaci�n)
    public Vector3 currentWindDirection; // Direcci�n ajustada seg�n la posici�n del jugador

    private UltimatePlayerController pController;
    private GravityBody gravityController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindWithTag("MainPlanet").transform;
        pController = GetComponent<UltimatePlayerController>();
        gravityController = GetComponent<GravityBody>();

        windIntervalCounter = windInterval;

        // Definir una direcci�n inicial del viento
        SetInitialWindDirection();
    }

    private void Update()
    {
        if (canGrip && Input.GetButton("Interact"))
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
        // Ajustar la direcci�n del viento en funci�n de la rotaci�n del jugador
        AdjustWindDirection();

        if (windIntervalCounter > 0)
        {
            windIntervalCounter -= Time.deltaTime;

            if (windIntervalCounter <= 0)
            {
                windDurationCounter = windDuration;
                pController.speed = pController.maxSpeed / 4;
                //pController.canJump = false;
            }
        }
        else if (windDurationCounter > 0)
        {
            //if (gravityController.gravityForce < gravityController.defaultGravityForce * 2.5f)
            //    gravityController.gravityForce += Time.deltaTime * 1000f;

            if (windEnabled)
            {
                ApplyWindForce();
            }

            windDurationCounter -= Time.deltaTime;

            if (windDurationCounter <= 0)
            {
                windIntervalCounter = windInterval;
                pController.speed = pController.maxSpeed;
                //pController.canJump = true;
                //gravityController.gravityForce = gravityController.defaultGravityForce;
            }
        }
    }

    void SetInitialWindDirection()
    {
        // Obtener vector de direcci�n desde el planeta al jugador (radial)
        Vector3 radialDirection = (rb.position - planet.position).normalized;

        // Calcular una direcci�n tangencial aleatoria
        baseWindDirection = Vector3.Cross(radialDirection, Random.onUnitSphere).normalized;

        // Aleatorizar la inversi�n de la direcci�n del viento
        if (Random.value > 0.5f)
        {
            baseWindDirection = -baseWindDirection;
        }
    }

    void AdjustWindDirection()
    {
        // Mantener la direcci�n del viento adaptada a la rotaci�n del jugador
        Vector3 radialDirection = (rb.position - planet.position).normalized;
        currentWindDirection = Vector3.Cross(radialDirection, baseWindDirection).normalized;
    }

    void ApplyWindForce()
    {
        // Aplicar la fuerza en la direcci�n ajustada
        rb.AddForce(currentWindDirection * windForce * Time.deltaTime, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GripZone"))
        {
            canGrip = true;
        }
        else if (other.CompareTag("WindZone"))
        {
            // Cambiar la direcci�n del viento a la del forward del trigger
            baseWindDirection = other.transform.forward.normalized;
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