using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    
    [HideInInspector]  AudioSource rocketSFX;

    public InputAction inputThrust;
    public InputAction inputRotation;

    [SerializeField] float thrustForce = 1000.0f;
    [SerializeField] float rotationForce = 1000.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rocketSFX = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        inputThrust.Enable();
        inputRotation.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Thrusting();

        Rotating();
    }

    void Thrusting()
    {
        if (inputThrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
            if (rocketSFX.isPlaying == false)
            {
                rocketSFX.Play();
            }
        }
        else
        {
            rocketSFX.Stop();
        }
            
    }

    void Rotating()
    {
        rb.freezeRotation = true;
        float inputRotationValue = inputRotation.ReadValue<float>();
        transform.Rotate(Vector3.forward * inputRotationValue * rotationForce * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    
     
}
