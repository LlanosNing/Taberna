using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UltimatePlayerController : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public float jumpForce;
    public float bounceForce;
    public bool antiGravity;
    public bool canMove = true;

    private Rigidbody rb;
    public Transform currentPlanet;
    public Transform playerVisual;

    public Vector3 normalVector;
    Vector3 input;
    GravityBody gravityScript;

    [Header("GROUND CHECK")]
    public bool isGrounded;
    public Collider[] detectedColliders;
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    public LayerMask groundLayer;

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    private Transform MainCameraTransform;
    public Transform CameraArmTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MainCameraTransform = Camera.main.transform;

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        gravityScript = GetComponent<GravityBody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        GroundCheck();

        if (Input.GetButtonDown("Jump") && canMove)
        {
            Jump();
        }
        if (currentPlanet != null)
        {
            //normalVector = (transform.position - currentPlanet.position).normalized;
            normalVector = -gravityScript.GravityDirection;
        }
    }

    private void FixedUpdate()
    {
        if (currentPlanet != null)
        {
            //normalVector = (transform.position - currentPlanet.position).normalized;
            normalVector = -gravityScript.GravityDirection;
        }

        if (canMove)
        {
            Vector3 cameraRotation = new Vector3(0, MainCameraTransform.localEulerAngles.y + CameraArmTransform.localEulerAngles.y, 0);
            Vector3 Dir = Quaternion.Euler(cameraRotation) * input.normalized;
            Vector3 movement_dir = (transform.forward * Dir.z + transform.right * Dir.x);
            Vector3 currentNormalVelocity = Vector3.Project(rb.velocity, normalVector.normalized);


            rb.velocity = currentNormalVelocity + (movement_dir * speed);

            if (movement_dir != Vector3.zero)
            {
                //anim.SetBool("IsMoving", true);
                playerVisual.localRotation = Quaternion.LookRotation(Dir);
            }
            else
            {
                //anim.SetBool("IsMoving", false);
            }

            ApplyPlanetRotation();
        }
    }

    void ApplyPlanetRotation()
    {
        if (!antiGravity) 
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, normalVector) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotSpeed * Time.fixedDeltaTime);        
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity *= 0;
            rb.AddForce(normalVector * jumpForce, ForceMode.Impulse);
        }
    }

    public void Bounce()
    {
        rb.velocity = Vector3.zero;

        rb.AddForce(normalVector * bounceForce, ForceMode.Impulse);
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        rb.velocity = Vector3.zero;
    }

    private void GroundCheck()
    {
        detectedColliders = Physics.OverlapBox(groundCheckCenter.position, groundCheckSize * 0.5f, Quaternion.Euler(0, 0, 0), groundLayer);

        if (detectedColliders.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
