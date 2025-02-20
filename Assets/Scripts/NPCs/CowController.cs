using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CowController : MonoBehaviour
{
    public Transform planet; // Referencia al planeta
    public float moveSpeed = 5f; // Velocidad de movimiento
    public bool isMoving;
    public float rotationSpeed = 5f; // Velocidad de rotación
    public LayerMask groundLayer; // Capa del suelo

    private Rigidbody rb;
    public Vector3 hitDirection;
    Vector3 moveDirection;
    public float hitRequiredDistance;
    public Transform cowVisual;
    Transform playerTransform;

    public float maxMovingDuration = 2.5f;
    float movingTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivar la gravedad de Unity para usar la propia
        rb.freezeRotation = true; // Evitar rotaciones raras

        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        // 1. Hacer Raycast hacia abajo para obtener la normal de la superficie
        Vector3 gravityDirection = (transform.position - planet.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -gravityDirection, out hit, 10f, groundLayer))
        {
            Vector3 surfaceNormal = hit.normal;

            // 2. Alinear al NPC con la superficie
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);            
        }

        if ((transform.position - playerTransform.position).magnitude < hitRequiredDistance && Input.GetButtonDown("Interact"))
        {
            MoveCowToDirection(playerTransform.position);
        }

        if (isMoving)
        {
            moveDirection = (transform.forward * hitDirection.z + transform.right * hitDirection.x).normalized;
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

            Quaternion lookDirection = Quaternion.LookRotation(moveDirection, cowVisual.up);
            cowVisual.rotation = Quaternion.Slerp(cowVisual.rotation, lookDirection, Time.deltaTime * rotationSpeed);
        }

        if(movingTimer > 0)
        {
            movingTimer -= Time.deltaTime;
        }

        if(movingTimer <= 0f)
        {
            StopCow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cow"))
        {
            other.GetComponent<CowController>().MoveCowToDirection(transform.position);
        }
    }

    public void StopCow()
    {
        isMoving = false;
        hitDirection = Vector3.zero;
    }

    public void MoveCowToDirection(Vector3 chaser)
    {
        hitDirection = (transform.position - chaser).normalized;
        movingTimer = maxMovingDuration;
        isMoving = true;
    }
}
