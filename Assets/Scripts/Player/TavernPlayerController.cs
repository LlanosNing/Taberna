using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TavernPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookRotSpeed = 10f;
    public bool canMove;
    Vector2 input;
    public Transform cameraTransform; // Arrastra aquí la cámara fija
    CharacterController controller;
    public Transform playerVisual;
    Animator animRef;

    public Sprite thePlayerSprite;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        animRef = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Move();
    }

    void Move()
    {
        if(canMove)
        {
            Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

            // Convertir la dirección de entrada a espacio relativo a la cámara
            Vector3 moveDir = cameraTransform.forward * direction.z + cameraTransform.right * direction.x;
            moveDir.y = 0f; // Evitar que el personaje se incline

            controller.Move(moveDir * moveSpeed * Time.deltaTime);

            Quaternion lookDirection = Quaternion.LookRotation(moveDir);
            if (moveDir != Vector3.zero)
            {
                playerVisual.localRotation = Quaternion.Slerp(playerVisual.localRotation, lookDirection, Time.deltaTime * lookRotSpeed);
            }

            float HVMagnitud = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude;

            animRef.SetFloat("HV_Magnitud", HVMagnitud);
        }
    }
}
