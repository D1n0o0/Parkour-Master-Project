using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float changeSceneDelay = 1.0f;
    [SerializeField] AudioClip explodeSFX;
    [SerializeField] AudioClip winSFX;

    bool playerWon;
    PlayerMovement controller;
    AudioSource rocketSFX;

    private void Awake()
    {
        controller = GetComponent<PlayerMovement>();
        rocketSFX = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            default:
                playerWon = false;
                rocketSFX.Stop();
                rocketSFX.PlayOneShot(explodeSFX, 0.1f);
                ManageLevel();
                break;

            case "Friendly":
                Debug.Log("This is friendly.");
                break;

            case "Finish":
                playerWon = true;
                rocketSFX.Stop();
                rocketSFX.PlayOneShot(winSFX);
                ManageLevel();
                break;
        }
    }

    void ManageLevel()
    {

        controller.enabled = false;
        Invoke("LoadLevel", changeSceneDelay);
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
