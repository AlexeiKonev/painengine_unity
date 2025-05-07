using UnityEngine;
using UnityEngine.InputSystem;



    public class PlayerController : MonoBehaviour
    {
        private PlayerInput playerInput;
        private Vector2 moveDirection;

        void Awake()
        {
            playerInput = new PlayerInput(); // Создаем экземпляр класса Input Actions
            
        }

        void OnEnable()
        {
            playerInput.Player.Enable(); // Включаем Action Map "Player"
            // Подписываемся на события
            playerInput.Player.Move.performed += OnMove;
            playerInput.Player.Move.canceled += OnMove; // Чтобы остановить движение, когда отпускаем клавишу
            playerInput.Player.Jump.performed += OnJump;
            playerInput.Player.Fire.performed += OnFire;
        }

        void OnDisable()
        {
            playerInput.Player.Disable(); // Выключаем Action Map "Player"
            // Отписываемся от событий
            playerInput.Player.Move.performed -= OnMove;
            playerInput.Player.Move.canceled -= OnMove;
            playerInput.Player.Jump.performed -= OnJump;
            playerInput.Player.Fire.performed -= OnFire;
        }

        void Update()
        {
            // Используем moveDirection в Update для движения
            Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * 5f;
            transform.Translate(movement);
        }

        // Обработчики событий
        private void OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
            Debug.Log("Move: " + moveDirection);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("Jump!");
            // Здесь добавь код для прыжка
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            Debug.Log("Fire!");
            // Здесь добавь код для стрельбы
        }
    }
