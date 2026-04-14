using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// get input from player.
    /// </summary>
    InputSystem_Actions inputActions;
    Vector2 moveInput;

    [SerializeField]
    float speed = 5.0f;
    Vector3 moveDirection;

    [SerializeField]
    public bool isColliding = false;
    float zValue = 6.0f;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }
    void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += ctx =>
        {
            moveInput = ctx.ReadValue<Vector2>();
        };

        inputActions.Player.Move.canceled += ctx =>
        {
            moveInput = Vector2.zero;
        };

    }
    void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("projectile"))
        {
            isColliding = true;
            float rbSpeed = -3.0f;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(moveInput.x * rbSpeed, zValue, moveInput.y * rbSpeed), ForceMode.Impulse);
        }     
    }
    /// <summary> Cách viết khác của OnCollisionEnter:
    /// private void OnCollisionEnter(Collision other)
    /// if(other.gameObject.CompareTag("wall")){
    /// ...
    /// ..
    /// .
    /// }
    /// </summary>

    // Update is called once per frame
    void Update()
    {
        if (isColliding == false)
        {
            moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }
}
