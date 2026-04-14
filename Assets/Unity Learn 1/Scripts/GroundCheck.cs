using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Rigidbody rb;
    PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            playerController.isColliding = false;
        }
    }
}
