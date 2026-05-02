using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float changeSceneDelay = 1.0f;
    [SerializeField] float bounceBackForce = 1000.0f;
    [SerializeField] AudioClip explodeSFX;
    [SerializeField] AudioClip winSFX;
    [SerializeField] ParticleSystem explodeVFX;
    [SerializeField] ParticleSystem winVFX;

    Rigidbody rb;
    PlayerMovement controller;
    AudioSource rocketSFX;

    bool playerWon;
    bool canPlayed;
    bool canCollide = true;

    private void Awake()
    {
        canPlayed = true;

        controller = GetComponent<PlayerMovement>();
        rocketSFX = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ChangeSceneToKey();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canPlayed || !canCollide) {
            return;
        }

        switch (collision.gameObject.tag)
        {
            default:
                playerWon = false;
                ChangeSceneToCollision(playerWon);
                break;

            case "Friendly":
                Debug.Log("do nothing");
                break;

            case "Finish":
                playerWon = true;
                ChangeSceneToCollision(playerWon);
                break;
        }
    }

    void ChangeSFX()
    {
        if (playerWon)
        {
            rocketSFX.Stop();
            rocketSFX.PlayOneShot(winSFX);
        }
        else
        {
            rocketSFX.Stop();
            rocketSFX.PlayOneShot(explodeSFX, 0.1f);
        }
    }

    void ChangeVFX()
    {
        if (playerWon)
        {
            winVFX.Play();
        }
        else
        {
            explodeVFX.Play();
        }
    }

    void ManageLevel()
    {
        controller.enabled = false;
        Invoke("LoadLevel", changeSceneDelay);
    }

    void DestroyedCastOff()
    {
        rb.AddRelativeForce((Vector3.up - Vector3.right) * bounceBackForce * Time.deltaTime,ForceMode.Impulse);
    }

    void LoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;       

        if (playerWon)
        {
            currentSceneIndex++;
            if (currentSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                currentSceneIndex = 0;
            }
            SceneManager.LoadScene(currentSceneIndex);
        }
        else { 
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
    void ChangeSceneToCollision(bool isWon)
    {
        if (isWon == false)
        {
            DestroyedCastOff();
        }

        ChangeSFX();
        ChangeVFX();
        ManageLevel();

        canPlayed = false;
    }

    void ChangeSceneToKey()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            playerWon = true;
            LoadLevel();
        }

        else if(Keyboard.current.kKey.wasPressedThisFrame)
        {
            canCollide = false;
        }
    }
}
