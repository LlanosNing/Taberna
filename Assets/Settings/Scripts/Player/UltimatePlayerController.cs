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
    public float coyoteTime;
    float coyoteTimeCounter;
    public Collider[] detectedColliders;
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    public LayerMask groundLayer;
    public bool canGroundPoundCarrot;

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    private Transform MainCameraTransform;
    public Transform CameraArmTransform;

    public Animator animRef;
    GravityBody gravityBodyRef;
    public bool isDancing;

    public Sprite[] thePlayerSprites;

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

        gravityBodyRef = GetComponent<GravityBody>();
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

        if (Input.GetButtonDown("Jump") && canMove && canJump && animRef.GetCurrentAnimatorStateInfo(0).IsTag("Locomotion"))
        {
            Jump();
        }

        if (Input.GetButtonDown("GroundPound") && canMove && !isGrounded)
        {
            GroundPound();
        }

        if (currentPlanet != null)
        {
            //normalVector = (transform.position - currentPlanet.position).normalized;
            normalVector = -gravityScript.GravityDirection;
        }


        if(coyoteTimeCounter > 0 && !isGrounded)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //if (Input.GetButton("Run") && isGrounded)
        //{
        //    speed = maxSpeed * 1.5f;
        //}

        //if (Input.GetButtonUp("Run"))
        //{
        //    speed = maxSpeed;
        //}

        //if(Input.GetKeyDown(KeyCode.F2)) 
        //{
        //    Dance();
        //}
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

            if (Physics.Raycast(transform.position, playerVisual.forward, 1, groundLayer))
            {

            }
            else
            {
                rb.velocity = currentNormalVelocity + (movement_dir * speed);
            }

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
        if (isGrounded || coyoteTimeCounter > 0)
        {
            rb.velocity *= 0;
            rb.AddForce(normalVector * jumpForce, ForceMode.Impulse);
            AudioManager.aMRef.PlaySFX(2);
        }
    }

    void GroundPound()
    {
        StartCoroutine(GroundPoundCO());
    }

    IEnumerator GroundPoundCO()
    {
        canJump = false;
        canMove = false;
        gravityBodyRef.gravityForce = 0f;
        rb.velocity = Vector3.zero;
        animRef.SetTrigger("GroundPound");
        yield return new WaitForSeconds(0.5f);
        gravityBodyRef.gravityForce = gravityBodyRef.defaultGravityForce;
        rb.AddForce(normalVector * -jumpForce * 2.5f, ForceMode.Impulse);
        canGroundPoundCarrot = true;
        yield return new WaitForSeconds(0.10f);
        animRef.SetTrigger("GroundPoundOff");
        canMove = true;
        canJump = true;        
        canGroundPoundCarrot = false;
    }

    public void Bounce(float bounceForce)
    {
        rb.velocity = Vector3.zero;

        rb.AddForce(normalVector * bounceForce, ForceMode.Impulse);

        AudioManager.aMRef.PlaySFX(12);
    }

    public void Impulse(Vector3 impulseDirection, float impulseForce)
    {
        rb.velocity = Vector3.zero;

        rb.AddForce((impulseDirection * 2 + normalVector.normalized) * impulseForce, ForceMode.Impulse);
    }

    public void HeadButt()
    {
        animRef.SetTrigger("Headbutt");
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
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            isGrounded = false;
        }
    }
}
