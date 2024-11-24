using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header ("MOVEMENT")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float bounceForce = 20f;

    Vector3 normalVector;
    public bool slowDown;
    PlayerControls playerControls;

    [Header ("CAMERA")]
    //La rotacion de la camara en el eje X
    public float camXRot = 0f;
    public float rotationSpeed = 200f;
    public float changePlayerDirectionSpeed = 5f;

    public Transform playerVisual;
    public Transform cameraArmTransform;

    [Header("GROUND CHECKER")]
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    public bool isGrounded = true;
    //Para guardar los colliders que detecta el ground checker
    private Collider[] detectedColliders;
    //Para que el ground checker solo detecte la layer que nos interesa (Ground)
    //Asi no detectara al Player ni otros objetos que estorban
    public LayerMask groundLayer;

    //Para guardar el input en Update y usarlo en FixedUpdate
    private Vector3 input;
    private Rigidbody rb;
    private Camera cam;

    [Header("RESPAWN POINT")]
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    void Start()
    {
        //Para buscar y asignar el Rigidbody automaticamente
        rb = GetComponent<Rigidbody>();
        //Buscamos la camara entre los objetos hijo y la asignamos
        cam = GetComponentInChildren<Camera>();

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        //playerControls.Movement.GamepadEastButton.performed += Jump();
    }

    void Update()
    {
        GroundCheck();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Probando probando");
            Jump();
        }
    }

    private void FixedUpdate()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 cameraRotation = new Vector3(0, Camera.main.transform.localEulerAngles.y + cameraArmTransform.localEulerAngles.y, 0);
        Vector3 Dir = Quaternion.Euler(cameraRotation) * input;
        Vector3 movement_dir = (transform.forward * Dir.z + transform.right * Dir.x);
        Vector3 currentNormalVelocity = Vector3.Project(rb.velocity, normalVector.normalized);
        rb.velocity = currentNormalVelocity + (movement_dir * moveSpeed);
        //rb.velocity = new Vector3(currentNormalVelocity.x + (movement_dir.x * moveSpeed), rb.velocity.y, currentNormalVelocity.z + (movement_dir.z * moveSpeed));

        if (movement_dir != Vector3.zero)
        {
            //anim.SetBool("IsMoving", true);
            playerVisual.localRotation = Quaternion.LookRotation(Dir);
        }
        else
        {
            //anim.SetBool("IsMoving", false);
        }
        if (slowDown)
            rb.velocity *= .5f;
    }

    void GroundCheck()
    {

        detectedColliders = Physics.OverlapBox(groundCheckCenter.position,
            groundCheckSize * 0.5f, groundCheckCenter.rotation, groundLayer);

        //Cuando el checker detecte al menos un objeto suelo, podemos saltar
        if (detectedColliders.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        if (!isGrounded) return;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        //rb.AddForce(normalVector * jumpForce, ForceMode.Impulse);

        //gravity = tmpGravity / 2f;
        //Invoke(nameof(RestoreGravity), 1f);
        //rotationSpeed = tmpRotationSpeed / 2f;
    }

    public void Bounce()
    {
        rb.velocity = Vector3.zero;

        rb.velocity = transform.up * bounceForce;
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        rb.velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(groundCheckCenter.localPosition, groundCheckSize);
    }
}
