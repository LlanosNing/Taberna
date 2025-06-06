using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CowController : MonoBehaviour
{
    public Transform planet; // Referencia al planeta
    public float moveSpeed = 5f; // Velocidad de movimiento
    public bool isMoving;
    public float rotationSpeed = 5f; // Velocidad de rotaci�n
    public LayerMask groundLayer; // Capa del suelo

    private Rigidbody rb;
    public Vector3 hitDirection;
    Vector3 moveDirection;
    public float hitRequiredDistance;
    public Transform cowVisual;
    Transform playerTransform;
    UltimatePlayerController playerController;

    public float maxMovingDuration = 2.5f;
    float movingTimer;

    public Animator cowAnim;

    public GameObject interactMessage;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivar la gravedad de Unity para usar la propia
        rb.freezeRotation = true; // Evitar rotaciones raras

        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
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

        if ((transform.position - playerTransform.position).magnitude < hitRequiredDistance)
        {
            interactMessage.SetActive(true);

            if (Input.GetButtonDown("Interact"))
            {
                MoveCowToDirection(playerTransform.position);
                AudioManager.aMRef.PlaySFX(4);
                playerController.HeadButt();
            }
        }
        else if((transform.position - playerTransform.position).magnitude < hitRequiredDistance * 1.5)
        {
            interactMessage.SetActive(false);
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
        if(other.CompareTag("Cow") && TryGetComponent(out CowController cow))
        {
            if(!cow.isMoving)
                cow.MoveCowToDirection(playerTransform.position);
        }

        if(other.CompareTag("Mole"))
            MoveCowToDirection(other.transform.position);
    }

    public void StopCow()
    {
        if(isMoving)
        {
            cowAnim.SetBool("IsMoving", false);
        }
        isMoving = false;
        hitDirection = Vector3.zero;
    }

    public void MoveCowToDirection(Vector3 chaser)
    {
        hitDirection = (transform.position - chaser).normalized;
        AudioManager.aMRef.PlaySFX(9);
        movingTimer = maxMovingDuration;
        isMoving = true;
        cowAnim.SetBool("IsMoving", true);
    }
}
