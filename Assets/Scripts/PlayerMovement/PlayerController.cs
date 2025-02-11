using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    // La rotaci�n de la c�mara en el eje X (arriba/abajo)
    public float camXRot = 0f;
    public float jumpForce = 700f;
    public bool isGrounded = true;
    // El pivote de la c�mara, pero ahora es solo para rotaci�n vertical de la c�mara
    public Transform cameraPivot;

    [Header("GROUND CHECKER")]
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    public Collider[] detectedColliders;
    public LayerMask groundLayer;

    private Vector3 input;
    public Rigidbody rb;
    private Camera cam;

    // Variables para la posici�n de la c�mara
    public Vector3 cameraOffset = new Vector3(0, 5, -10);  // Posici�n en relaci�n al jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main; // Usamos la c�mara principal de la escena
    }

    void Update()
    {
        HandleMovementInput();
        HandleCameraInput();
        HandleJump();
        GroundCheck();
    }

    private void HandleMovementInput()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        input = new Vector3(_horizontal, 0f, _vertical);
        input = transform.TransformDirection(input);
    }

    private void HandleCameraInput()
    {
        // Rotaci�n horizontal del jugador
        float _rotMouseX = Input.GetAxisRaw("Mouse X");
        transform.Rotate(0, _rotMouseX * rotationSpeed * Time.deltaTime, 0);

        // Rotaci�n vertical de la c�mara
        camXRot -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;
        camXRot = Mathf.Clamp(camXRot, -60, 60);
        cameraPivot.localEulerAngles = new Vector3(camXRot, 0, 0);

        // Ajuste de la c�mara en el espacio 3D
        Vector3 desiredCameraPos = transform.position + cameraOffset;
        cam.transform.position = Vector3.Lerp(cam.transform.position, desiredCameraPos, Time.deltaTime * 5f);
        cam.transform.LookAt(transform.position + Vector3.up); // Mant�n la c�mara mirando hacia el jugador
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void FixedUpdate()
    {
        Vector3 _velocity = input.normalized * moveSpeed;
        _velocity.y = rb.velocity.y;
        rb.velocity = _velocity;
    }

    void GroundCheck()
    {
        detectedColliders = Physics.OverlapBox(groundCheckCenter.position, groundCheckSize * 0.5f, Quaternion.Euler(0, 0, 0), groundLayer);
        isGrounded = detectedColliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheckCenter.position, groundCheckSize);
    }
}



