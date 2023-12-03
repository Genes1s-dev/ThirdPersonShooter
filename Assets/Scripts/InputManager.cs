using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;              
    public PlayerInput.PlayerMapActions inputActions;       //ссылка на экшн мапу
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    private PlayerHealth playerHealth;
    

    private void Awake()
    {
        playerInput = new PlayerInput();            //создаём экземпляр класса
        inputActions = playerInput.PlayerMap;
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        playerHealth = GetComponent<PlayerHealth>();

        inputActions.Jump.performed += ctx => playerMotor.Jump();
        inputActions.Sprint.performed += ctx => playerMotor.Sprint();
        inputActions.Aim.performed += ctx => playerMotor.Aim();
        inputActions.Fire.performed += ctx => playerMotor.Fire();
        inputActions.SwitchWeapon.performed += ctx => playerMotor.SwitchWeapon();

        //test HP bar change
        inputActions.HealthBarChange_test.performed += ctx => playerHealth.TakeDamage(Random.Range(5, 20));
    }

    private void OnDestroy()
    {
        inputActions.Jump.performed -= ctx => playerMotor.Jump();
        inputActions.Sprint.performed -= ctx => playerMotor.Sprint();
        inputActions.Aim.performed -= ctx => playerMotor.Aim();
        inputActions.Fire.performed -= ctx => playerMotor.Fire();
        inputActions.SwitchWeapon.performed -= ctx => playerMotor.SwitchWeapon();
    }

    private void FixedUpdate()
    {
        //двигаем плэермотор, используя полученное значение из нашего movement action
        playerMotor.ProcessMove(inputActions.Movement.ReadValue<Vector2>()); 
    }

    private void LateUpdate()
    {
        //вращаем игрока, используя полученное значение из Look action
        playerLook.ProcessLook(inputActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
