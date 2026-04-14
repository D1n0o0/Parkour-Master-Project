using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    PlayerMovement controller;
    [SerializeField] float changeSceneDelay = 1.0f;
    bool isSuccess;

    private void Awake()
    {
        controller = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            default:
                isSuccess = false;
                ManageLevel();
                break;
            case "Friendly":
                Debug.Log("This is friendly.");
                break;
            case "Finish":
                isSuccess = true;
                ManageLevel();
                break;
        }
    }

    void ManageLevel()
    {
        controller.inputThrust.Disable();
        controller.inputRotation.Disable();
        Invoke("LoadLevel", changeSceneDelay);
    }

    void LoadLevel()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;       

        switch (isSuccess){
            case true:
                currentSceneIndex++;
                if (currentSceneIndex >= SceneManager.sceneCountInBuildSettings){
                    currentSceneIndex = 0;
                }
                SceneManager.LoadScene(currentSceneIndex);
                break;

            case false:
                SceneManager.LoadScene(currentSceneIndex);
                break;
        }     
    }
}
