using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool canHit;
    public Transform cowVisual;
    Transform playerTransform;

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

            if(canHit && Input.GetButtonDown("Interact"))
            {
                hitDirection = (transform.position - playerTransform.position).normalized;
                isMoving = true;
            }

            if (isMoving)
            {
                moveDirection = (transform.forward * hitDirection.z + transform.right * hitDirection.x).normalized;
                rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

                Quaternion lookDirection = Quaternion.LookRotation(moveDirection, cowVisual.up);
                cowVisual.rotation = Quaternion.Slerp(cowVisual.rotation, lookDirection, Time.deltaTime * rotationSpeed);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canHit = false;
        }
    }
}
