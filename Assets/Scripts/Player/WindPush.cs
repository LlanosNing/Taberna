using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
    private Vector3 baseWindDirection; // Dirección base del viento (sin ajuste por rotación)
    public Vector3 currentWindDirection; // Dirección ajustada según la posición del jugador

    private UltimatePlayerController pController;
    private GravityBody gravityController;

    public Volume sandstormVolume;
    public float warningWeight, sandstormWeight;

    public Transform sandstormParent;

    Sandstorm_Animation animController;

    public GameObject gripSignUI;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindWithTag("MainPlanet").transform;
        pController = GetComponent<UltimatePlayerController>();
        gravityController = GetComponent<GravityBody>();
        animController = GetComponent<Sandstorm_Animation>();

        windIntervalCounter = windInterval;

        // Definir una dirección inicial del viento
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
        // Ajustar la dirección del viento en función de la rotación del jugador
        AdjustWindDirection();

        if (windIntervalCounter > 0)
        {
            if(windEnabled)
            {
                windIntervalCounter -= Time.deltaTime;
            }

            if (windIntervalCounter <= 0)
            {
                windDurationCounter = windDuration;
                pController.speed = pController.maxSpeed / 4;

                //pController.canJump = false;
            }

            if(windIntervalCounter < 3f)
            {
                sandstormVolume.weight = Mathf.MoveTowards(sandstormVolume.weight, warningWeight, Time.deltaTime * 5);

                RenderSettings.fogDensity = Mathf.MoveTowards(RenderSettings.fogDensity, 0.02f, Time.deltaTime * 5);


                if (!animController.animationOn) 
                { 
                    animController.StartPartycleSystem();
                }
            }
            else
            {
                sandstormVolume.weight = Mathf.MoveTowards(sandstormVolume.weight, 0f, Time.deltaTime * 5);

                RenderSettings.fogDensity = Mathf.MoveTowards(RenderSettings.fogDensity, 0, Time.deltaTime * 5);
            }
        }
        else if (windDurationCounter > 0)
        {
            //if (gravityController.gravityForce < gravityController.defaultGravityForce * 2.5f)
            //    gravityController.gravityForce += Time.deltaTime * 1000f;

            if (windEnabled)
            {
                ApplyWindForce();
                sandstormVolume.weight = Mathf.MoveTowards(sandstormVolume.weight, sandstormWeight, 0.2f * Time.deltaTime);
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
        // Obtener vector de dirección desde el planeta al jugador (radial)
        Vector3 radialDirection = (rb.position - planet.position).normalized;

        // Calcular una dirección tangencial aleatoria
        baseWindDirection = Vector3.Cross(radialDirection, Random.onUnitSphere).normalized;

        // Aleatorizar la inversión de la dirección del viento
        if (Random.value > 0.5f)
        {
            baseWindDirection = -baseWindDirection;
        }
    }

    void AdjustWindDirection()
    {
        // Mantener la dirección del viento adaptada a la rotación del jugador
        Vector3 radialDirection = (rb.position - planet.position).normalized;
        currentWindDirection = Vector3.Cross(radialDirection, baseWindDirection).normalized;

        sandstormParent.rotation = Quaternion.LookRotation(currentWindDirection);
    }

    void ApplyWindForce()
    {
        // Aplicar la fuerza en la dirección ajustada
        if(windEnabled)
        {
            rb.AddForce(currentWindDirection * windForce * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GripZone"))
        {
            canGrip = true;
        }
        else if (other.CompareTag("WindZone"))
        {
            // Cambiar la dirección del viento a la del forward del trigger
            baseWindDirection = other.transform.forward.normalized;
            windEnabled = true;
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