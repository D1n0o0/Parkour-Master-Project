using UnityEngine;
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

    private void Awake()
    {
        canPlayed = true;

        controller = GetComponent<PlayerMovement>();
        rocketSFX = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canPlayed) { return; }

        switch (collision.gameObject.tag)
        {
            default:
                playerWon = false;
                ChangeSFX();
                ChangeVFX();
                CastOff();
                ManageLevel();
                canPlayed = false;
                break;

            case "Friendly":
                Debug.Log("do nothing");
                break;

            case "Finish":
                playerWon = true;
                ChangeSFX();
                ChangeVFX();
                ManageLevel();
                canPlayed = false;
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

    void CastOff()
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
}
