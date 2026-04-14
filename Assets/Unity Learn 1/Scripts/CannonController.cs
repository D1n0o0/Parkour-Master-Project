using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    Transform player;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }


    void Start()
    {
        InvokeRepeating("InstantitateProjectile", Random.Range(3.0f, 5.0f), Random.Range(3.0f, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        RotateToPlayer();
    }

    void InstantitateProjectile()
    {
        GameObject newProjectile;
        newProjectile = Instantiate(projectile, shootPos.position, shootPos.rotation);
        newProjectile.GetComponent<Projectile>().shootPos = shootPos;
    }

    void RotateToPlayer()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
