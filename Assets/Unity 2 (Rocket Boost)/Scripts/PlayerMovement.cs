using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public InputAction inputThrust;
    public InputAction inputRotation;

    [SerializeField] float thrustForce = 1000.0f;
    [SerializeField] float rotationForce = 40.0f;
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] ParticleSystem thrustVFX;
    [SerializeField] ParticleSystem leftThrustVFX;
    [SerializeField] ParticleSystem rightThrustVFX;

    Rigidbody rb;
    AudioSource rocketSFX;

    private void OnEnable()
    {
        inputThrust.Enable();
        inputRotation.Enable();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rocketSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Thrusting();
        Rotating();
    }

    void Thrusting()
    {
        if (inputThrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

            /* áp dụng SFX và VFX */
            if (rocketSFX.isPlaying == false)
            {
                rocketSFX.PlayOneShot(thrustSFX);
            }
            if (thrustVFX.isPlaying == false)
            {
                thrustVFX.Play();
            }
        }
        else
        {
            /* dừng áp dụng SFX và VFX */
            rocketSFX.Stop();
            thrustVFX.Stop();
        }
    }
    void Rotating()
    {
        rb.freezeRotation = true;
        float inputRotationValue = inputRotation.ReadValue<float>();
        transform.Rotate(Vector3.forward * inputRotationValue * rotationForce * Time.fixedDeltaTime);

        /* khi xoay thì SFX ở bộ đẩy 2 bên trái phải sẽ play() */
        thrustersVFXcontrol(inputRotationValue);

        rb.freezeRotation = false;
    } 

    void thrustersVFXcontrol(float inputRotationValue)
    {
        if (inputRotationValue > 0)
        {
            if (rightThrustVFX.isPlaying == false)
            {
                rightThrustVFX.Play();
            }
        }
        else if (inputRotationValue < 0)
        {
            if (leftThrustVFX.isPlaying == false)
            {
                leftThrustVFX.Play();
            }
        }
        else
        {
            leftThrustVFX.Stop();
            rightThrustVFX.Stop();
        }
    }
}
