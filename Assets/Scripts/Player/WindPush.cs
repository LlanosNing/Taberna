using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WindPush : MonoBehaviour
{
    public float windForce = 20f;

    public float windInterval = 5f;
    public float windIntervalCounter;
    public float windDuration = 1.5f;
    public float windDurationCounter;

    public bool canGrip;
    public bool isGripped;
    public bool windEnabled;

    private Rigidbody rb;
    private Transform planet;
    private Vector3 baseWindDirection; // Direcci�n base del viento (sin ajuste por rotaci�n)
    public Vector3 currentWindDirection; // Direcci�n ajustada seg�n la posici�n del jugador

    private UltimatePlayerController pController;
    GravityBody gravityScript;
    private GravityBody gravityController;

    public Volume sandstormVolume;
    public float warningWeight, sandstormWeight;

    public Transform sandstormParent;

    Sandstorm_Animation animController;

    public GameObject gripSignUI;
    public TextMeshProUGUI gripSignUIText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindWithTag("MainPlanet").transform;
        gravityScript = GameObject.FindWithTag("Player").GetComponent<GravityBody>();
        pController = GetComponent<UltimatePlayerController>();
        gravityController = GetComponent<GravityBody>();
        animController = GetComponent<Sandstorm_Animation>();

        windIntervalCounter = windInterval;

        // Definir una direcci�n inicial del viento
        SetInitialWindDirection();
    }

    private void Update()
    {
        if (canGrip && Input.GetButton("Interact"))
        {
            isGripped = true;
            pController.canMove = false;
            rb.velocity = Vector3.zero;
            gravityScript.gravityForce = 0;

            gripSignUIText.color = new Color(1f, 1f, 0.5f);
        }

        if (Input.GetButtonUp("Interact"))
        {
            isGripped = false;
            pController.canMove = true;
            gravityScript.gravityForce = gravityScript.defaultGravityForce;

            gripSignUIText.color = new Color(0.5f, 1f, 0.75f);
        }
    }

    private void FixedUpdate()
    {
        // Ajustar la direcci�n del viento en funci�n de la rotaci�n del jugador
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
            if (windEnabled && !isGripped)
            {
                ApplyWindForce();
            }
            sandstormVolume.weight = Mathf.MoveTowards(sandstormVolume.weight, sandstormWeight, 0.2f * Time.deltaTime);

            windDurationCounter -= Time.deltaTime;

            if (windDurationCounter <= 0)
            {
                windIntervalCounter = windInterval;
                pController.speed = pController.maxSpeed;
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

        sandstormParent.rotation = Quaternion.LookRotation(currentWindDirection);
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

            gripSignUI.SetActive(true);
        }
        else if (other.CompareTag("WindZone"))
        {
            // Cambiar la direcci�n del viento a la del forward del trigger
            baseWindDirection = other.transform.forward.normalized;
            windEnabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GripZone"))
        {
            canGrip = false;

            gripSignUI.SetActive(false);
        }
    }
}