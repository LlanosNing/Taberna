using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header ("MOVEMENT")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float bounceForce = 20f;

    Vector3 normalVector;
    public bool slowDown;

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
    }

    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 cameraRotation = new Vector3(0, Camera.main.transform.localEulerAngles.y + cameraArmTransform.localEulerAngles.y, 0);
        Vector3 Dir = Quaternion.Euler(cameraRotation) * input;
        Vector3 movement_dir = (transform.forward * Dir.z + transform.right * Dir.x);
        Vector3 currentNormalVelocity = Vector3.Project(rb.velocity, normalVector.normalized);
        rb.velocity = currentNormalVelocity + (movement_dir * moveSpeed);
        

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
        //Guardamos el input para usarlo en FixedUpdate
        //input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        //Para que se mueva en la direccion correcta respecto hacia donde mira la cámara, transformamos el input para que sea en local, no en global
        //input.y = 0f;

        //El eje de movimiento del raton es el X, pero la rotacion del objeto es en el eje Y
        //float _rotMouseX = Input.GetAxisRaw("Mouse X");
        //transform.Rotate(0, _rotMouseX * rotationSpeed * Time.deltaTime, 0);

        //Hay que ir acumulando el valor de la rotacion en X de la camara para que aumente o disminuya conforme movemos el raton arriba y abajo
        //camXRot -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;

        //Limitamos el valor de la rotacion X a -60 y 60 grados
        //camXRot = Mathf.Clamp(camXRot, -60, 60);

        //Asignamos la rotacion en X a los angulos del pivote de la camara
        //cameraPivot.eulerAngles = new Vector3(camXRot, cameraPivot.eulerAngles.y, 0);
        //cameraPivot.Rotate(0, _rotMouseX * rotationSpeed * Time.deltaTime, 0f);

        //cameraPivot.position = transform.position;
        //cameraPivot.localEulerAngles = new Vector3(transform.localEulerAngles.x, cameraPivot.localEulerAngles.y, transform.localEulerAngles.z);

        //if(Input.GetAxis("Horizontal Dpad") > 0.1f || Input.GetAxis("Horizontal Dpad") < -0.1f)
        //{
        //    cameraPivot.localEulerAngles += new Vector3(0f, Input.GetAxis("Horizontal Dpad") * rotationSpeed * Time.deltaTime, 0f);
        //}

        //if(input != Vector3.zero)
        //{
        //Quaternion _rot = Quaternion.LookRotation(input, transform.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _rot, Time.deltaTime * changePlayerDirectionSpeed);
        //}

        //SALTO
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        GroundCheck();
    }

    private void FixedUpdate()
    {
        //Hay que normalizar el input para que no se mueva mas rapido en diagonal
        Vector3 _velocity = input.normalized * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + _velocity);
        //_velocity.y = rb.velocity.y;
        //rb.velocity = _velocity;
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
