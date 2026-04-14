using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public Transform shootPos;
    float projectileSpeed = 2.0f;
    GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }
    private void Start()
    {
        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, shootPos.position);
        Vector3 direction = shootPos.transform.forward.normalized;
        rb.AddForce(direction * distance * projectileSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Booomm");
        Destroy(gameObject);
    }
}
