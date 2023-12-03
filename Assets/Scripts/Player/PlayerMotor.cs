using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    public float speed = 3f;

    private bool isGrounded;
    public float gravity = -9.8f;

    public float jumpHeight = 3f;

    private bool sprinting;
    public bool aiming {get; private set;}
    private Inventory playerInventory;
    private BaseWeapon currentWeapon;
    private int index = 0;
    private new CameraHandler camera;
 

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = GetComponentInChildren<CameraHandler>();
        playerInventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

    }

    //получает входные данные для InputManager.cs и применяем их для character controller'a
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;  //конвертируем из двумерной системы координат входных данных в трёхмерные игровые  (вертикальной передвижение -> передвижение вперёд/назад)

        
        if (input == Vector2.zero || !isGrounded)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isSprinting", false);
        } else {
            if (sprinting && isGrounded)
            {
                animator.SetBool("isSprinting", true);
                animator.SetBool("isRunning", false);
            } else 
            if (isGrounded) {
                animator.SetBool("isRunning", true);
                animator.SetBool("isSprinting", false);
            }
        }

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);  //реализуем само передвижение

        animator.SetFloat("vertical", input.y);
        animator.SetFloat("horizontal", input.x);
        
        //реализуем применение гравитации к игроку
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            animator.SetBool("isRunning", false);
            animator.SetBool("isSprinting", false);
            animator.SetTrigger("Jump");
        }
    }


    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8f;
        } else {
            speed = 3f;
        }
    }

    public void SwitchWeapon()
    {
        if (currentWeapon == null && playerInventory.GetItemsList().Count > 0)
        {
            currentWeapon = playerInventory.GetItemsList()[0];
        }
        
        if (playerInventory.GetItemsList().Count > 0)
        {
            if (!currentWeapon.isReloading)
            {
                camera.ResetCamera();

                aiming = false;

                currentWeapon.gameObject.SetActive(false);
                index++;
                index = index % playerInventory.GetItemsList().Count; //устанавливаем в значение индекса остаток от деления, т.о. он никогда не будет >= размерности списка
                currentWeapon = playerInventory.GetItemsList()[index];
                currentWeapon.gameObject.SetActive(true);

                currentWeapon.UpdateUI();
            }
        }


    }
    public void Fire()
    {
        if (currentWeapon != null)
        {
            if (!currentWeapon.isReloading)
            {
                currentWeapon.Fire();
            }
        }
    }

    public void Aim()
    {
        if (currentWeapon != null)
        {
            aiming = !aiming;
            currentWeapon.Aim();
        }
    }
}
