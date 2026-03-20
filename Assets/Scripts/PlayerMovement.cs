using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 input;
    InputSystem_Actions inputActions;

    public CharacterController controller;
    public float CharacterSpeed = 5.0f;

    // Thời gian để xoay mượt mà khi thay đổi hướng di chuyển.
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        
    }
    private void OnEnable()
    {
        inputActions.Enable();

        //khi có Input (bấm phím), nó sẽ gọi hàm Move với giá trị của Input.
        inputActions.Player.Move.performed += ctx => input = ctx.ReadValue<Vector2>();

        //khi không có Input (thả phím), nó sẽ gọi hàm Move với giá trị Vector2.zero.
        inputActions.Player.Move.canceled += ctx => input = Vector2.zero;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {   
        /// <summary>
        /// 20/3/2026
        ///Di chuyển nhân vật dựa trên input từ bàn phím sử dụng input system.
        /// </summary>
        Vector3 moveInput = new Vector3(input.x, 0f, input.y).normalized;

        if(moveInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(moveInput * CharacterSpeed * Time.deltaTime);
        }               
    }
}
