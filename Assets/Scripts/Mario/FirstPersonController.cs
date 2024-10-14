using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("MOVEMENT")]
    public float speed = 5f;
    public float rotationSpeed = 200f;
    //Rotación de la cámara en el eje X
    public float camXRot = 0f;

    public float jumpForce = 10f;

    [Header("GROUND CHECKER")]
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;

    public bool isGrounded;
    public bool hasDoubleJumped;
    public Collider[] detectedColliders;
    public LayerMask groundLayer;


    //Para guardar el input en Update y usarlo en FixedUpdate
    private Vector3 input;

    private Rigidbody rb;

    private Camera cam;

    private GravityCharTest _grScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        _grScript = GetComponent<GravityCharTest>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //Para que los valores sean locales, hay que transformar el input:
        input = transform.TransformDirection(input);

        //El eje de movimiento del ratón es el X, pero la rotación del objeto es en el eje Y
        float _rotMouseX = Input.GetAxisRaw("Mouse X");
        transform.Rotate(0f, _rotMouseX * rotationSpeed * Time.deltaTime, 0f);

        //Hay que ir acumulando el valor de la rotación en X
        camXRot -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;
        //Límite del valor de la rotación
        camXRot = Mathf.Clamp(camXRot, -60f, 60f);

        cam.transform.localEulerAngles = new Vector3(camXRot, 0f, 0f);

        GroundCheck();

        //Salto
        if(Input.GetKeyDown(KeyCode.Space) && (isGrounded || !hasDoubleJumped)) 
        {
            //Reseteamos la fuerza de caída
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //Establecemos una fuerza hacia arriba
            
            rb.AddForce(-_grScript.gravityDirection * jumpForce); 

            hasDoubleJumped = true;
        }

        //Correr
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }

    }

    private void FixedUpdate()
    {
        Vector3 _velocity = input.normalized * speed;
        _velocity.y = rb.velocity.y; //Para que pueda caer correctamente

        rb.velocity = _velocity;
    }

    private void GroundCheck()
    {
        detectedColliders = Physics.OverlapBox(groundCheckCenter.position, groundCheckSize * 0.5f, Quaternion.Euler(0,0,0), groundLayer);

        if (detectedColliders.Length > 0) 
        {
            isGrounded = true;

            hasDoubleJumped = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    //Para representar los Overlaps visualmente
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckCenter.position, groundCheckSize);
    }
}
