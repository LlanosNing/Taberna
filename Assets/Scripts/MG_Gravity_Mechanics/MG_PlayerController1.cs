using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PlayerController1 : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    //[SerializeField] private Animator _animator;

    public float groundCheckRadius = 0.3f;
    public float speed = 8;
    public float turnSpeed = 1500f;

    public bool canMove = true;

    [Header("JUMP")]
    public float jumpForce = 500f;
    public bool isGrounded;
    public Collider[] detectedColliders;

    [Header("GROUND CHECKER")]
    public Transform groundCheckCenter;
    public Vector3 groundCheckSize = Vector3.one;
    public LayerMask groundLayer;

    [Header("REFERENCIAS")]
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private GravityBody _gravityBody;

    [Header("RESPAWN POINT")]
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    public Transform cameraPivot;
    public float camXRot = 0f;
    public float rotationSpeed = 200f;


    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
        _gravityBody = transform.GetComponent<GravityBody>();

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        cameraPivot = GameObject.Find("CameraPivot").transform;
    }

    void Update()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        //_direction = transform.TransformDirection(_direction);
        GroundCheck();
        //_animator.SetBool("isJumping", !isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

            _rigidbody.AddForce(-_gravityBody.GravityDirection * jumpForce, ForceMode.Impulse);
        }

        float _rotMouseX = Input.GetAxisRaw("Mouse X");
        transform.Rotate(0f, _rotMouseX * rotationSpeed * Time.deltaTime, 0f);

        //Hay que ir acumulando el valor de la rotación en X
        camXRot -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;
        //Límite del valor de la rotación
        camXRot = Mathf.Clamp(camXRot, -40f, 50f);

        cameraPivot.localEulerAngles = new Vector3(camXRot, 0f, 0f);
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            bool isRunning = _direction.magnitude > 0.1f;

            if (isRunning)
            {
                Vector3 directionX = transform.right * _direction.x;
                Vector3 directionZ = transform.forward * _direction.z;
               
                _rigidbody.MovePosition(_rigidbody.position + (directionX + directionZ) * (speed * Time.fixedDeltaTime));              
            }

            //_animator.SetBool("isRunning", isRunning);
        }
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

    public void Respawn()
    {
        transform.position = spawnPosition; 
        transform.rotation = spawnRotation;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckCenter.position, groundCheckSize);
    }
}
