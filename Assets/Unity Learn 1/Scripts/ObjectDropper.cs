using UnityEngine;

public class ObjectDropper : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer meshRenderer;
    float timer;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("The object has hit the ground.");
            /* change material when fall to the ground in the future */
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.tag = "Untagged";
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        timer = Random.Range(5.0f, 15.0f);
    }

    private void Start()
    {
        meshRenderer.enabled = false;
        rb.useGravity = false;
    }

    void Update()
    {
        
        if (Time.time > timer)
        {
            meshRenderer.enabled = true;
            rb.useGravity = true;
        }

        
    }
}
