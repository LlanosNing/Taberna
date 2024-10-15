using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _cam;
    //[SerializeField] private Animator _animator;

    public float groundCheckRadius = 0.3f;
    public float speed = 8;
    public float turnSpeed = 1500f;
    public float jumpForce = 500f;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private GravityBody _gravityBody;

    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
        _gravityBody = transform.GetComponent<GravityBody>();
    }

    void Update()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        bool isGrounded = Physics.CheckSphere(_groundCheck.position, groundCheckRadius, _groundMask);
        //_animator.SetBool("isJumping", !isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rigidbody.AddForce(-_gravityBody.GravityDirection * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        bool isRunning = _direction.magnitude > 0.1f;

        if (isRunning)
        {
            Vector3 direction = transform.forward * _direction.z;
            _rigidbody.MovePosition(_rigidbody.position + direction * (speed * Time.fixedDeltaTime));

            Quaternion rightDirection = Quaternion.Euler(0f, _direction.x * (turnSpeed * Time.fixedDeltaTime), 0f);
            Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, _rigidbody.rotation * rightDirection, Time.fixedDeltaTime * 3f); ;
            _rigidbody.MoveRotation(newRotation);
        }

        //_animator.SetBool("isRunning", isRunning);
    }
}
