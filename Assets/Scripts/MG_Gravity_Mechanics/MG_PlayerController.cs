using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationCameraSpeed = 400f;
    public float camXRot = 0f;
    public float jumpForce = 10f;
    public bool isGrounded = true;

    [Header("GROUND CHECKER")]
    public Transform groundCheckerCenter;
    public Vector3 groundCheckSize = Vector3.one;
    public Collider[] detectedColliders;
    public LayerMask groundLayer;

    //Para guardar el input en Update y usarlo en FixedUpdate
    private Vector3 input;
    private Rigidbody _rb;
    private Camera cam;

    [Header("RESPAWN POINT")]
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        cam = GetComponentInChildren<Camera>();

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    private void Update()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        input = new Vector3(_horizontal, 0f, _vertical);
        //Para que se mueva en la dirección correcta respecto hacia donde mira, hay que transformar el input para que sea en espacio local y no en espacio global.
        input = transform.TransformDirection(input);

        //El eje de movimiento del raton es el X, pero la rotación del objeto es en el eje Y
        float _rotMouseX = Input.GetAxisRaw("Mouse X");
        transform.Rotate(0, _rotMouseX * rotationCameraSpeed * Time.deltaTime, 0);

        //Hay que ir acumulando el valor de la rotación en X de la camara para que aumente o disminuya conforme movemos el ratón arriba y abajo.
        camXRot -= Input.GetAxisRaw("Mouse Y") * rotationCameraSpeed * Time.deltaTime;
        camXRot = Mathf.Clamp(camXRot, -80, 80);
        cam.transform.localEulerAngles = new Vector3(camXRot, 0, 0);


        //SALTO
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce);
        }

        GroundCheck();
    }

    private void FixedUpdate()
    {
        //normalized es para que no se mueva más rapido en diagonal
        Vector3 _velocity = input.normalized * moveSpeed;
        _velocity.y = _rb.velocity.y;
        _rb.velocity = _velocity;
    }

    void GroundCheck()
    {
        detectedColliders = Physics.OverlapBox(groundCheckerCenter.position, groundCheckSize * 0.5f, Quaternion.Euler(0, 0, 0), groundLayer);

        if (detectedColliders.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheckerCenter.position, groundCheckSize);
    }


    public void Respawn()
    {
        transform.position = spawnPosition; 
        transform.rotation = spawnRotation;
        _rb.velocity = Vector3.zero;
    }
}
