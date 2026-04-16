using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public InputAction inputThrust;
    public InputAction inputRotation;

    [SerializeField] float thrustForce = 1000.0f;
    [SerializeField] float rotationForce = 1000.0f;
    [SerializeField] AudioClip thrustSFX;

    Rigidbody rb;
    AudioSource rocketSFX;
    CollisionHandler collisionHandler;

    private void OnEnable()
    {
        inputThrust.Enable();
        inputRotation.Enable();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rocketSFX = GetComponent<AudioSource>();
        collisionHandler = GetComponent<CollisionHandler>();
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
                rocketSFX.PlayOneShot(thrustSFX);
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
