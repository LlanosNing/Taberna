using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class UltimatePlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float rotSpeed;
    public float lookRotSpeed;
    public float jumpForce;
    public bool antiGravity;
    public bool canMove = true;

    private Rigidbody rb;
    public Transform currentPlanet;
    public Transform playerVisual;

    public Vector3 normalVector;
    Vector3 input;
    GravityBody gravityScript;

    [Header("GROUND CHECK")]
    public bool canJump;
    public bool isGrounded;
    public Collider[] detectedColliders;
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    public LayerMask groundLayer;

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    private Transform MainCameraTransform;
    public Transform CameraArmTransform;

    public Animator animRef;
    public bool isDancing;

    public Sprite thePlayerSprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MainCameraTransform = Camera.main.transform;
        speed = maxSpeed;

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        gravityScript = GetComponent<GravityBody>();

        GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        float HVMagnitud = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude;

        if (!canMove)
        {
            HVMagnitud = 0;
        }

        animRef.SetFloat("HV_Magnitud", HVMagnitud);
        animRef.SetBool("isGrounded", isGrounded);

        GroundCheck();

        if (Input.GetButtonDown("Jump") && canMove  && canJump && animRef.GetCurrentAnimatorStateInfo(0).IsTag("Locomotion"))
        {
            Debug.Log("Salto");
            Jump();
        }
        if (currentPlanet != null)
        {
            //normalVector = (transform.position - currentPlanet.position).normalized;
            normalVector = -gravityScript.GravityDirection;
        }

        if(Input.GetKeyDown(KeyCode.F2)) 
        {
            Dance();
        }
    }

    private void FixedUpdate()
    {
        if (currentPlanet != null)
        {
            //normalVector = (transform.position - currentPlanet.position).normalized;
            normalVector = -gravityScript.GravityDirection;
        }

        if (canMove && animRef.GetCurrentAnimatorStateInfo(0).IsTag("Locomotion"))
        {
            Vector3 cameraRotation = new Vector3(0, MainCameraTransform.localEulerAngles.y + CameraArmTransform.localEulerAngles.y, 0);
            Vector3 Dir = Quaternion.Euler(cameraRotation) * input.normalized;
            Vector3 movement_dir = (transform.forward * Dir.z + transform.right * Dir.x);
            Vector3 currentNormalVelocity = Vector3.Project(rb.velocity, normalVector.normalized);


            rb.velocity = currentNormalVelocity + (movement_dir * speed);

            if (movement_dir != Vector3.zero)
            {
                Quaternion lookDirection = Quaternion.LookRotation(Dir);
                playerVisual.localRotation = Quaternion.Slerp(playerVisual.localRotation, lookDirection, Time.deltaTime * lookRotSpeed); 
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

    public void Bounce(float bounceForce)
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

    public void Dance()
    {
        if (isDancing) isDancing = false;
        else if (!isDancing) isDancing = true;

        animRef.SetBool("isDancing", isDancing);
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
