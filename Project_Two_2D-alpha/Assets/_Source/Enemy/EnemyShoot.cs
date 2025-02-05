using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingRange;
    public float shootingInterval;
    public LayerMask playerLayer;

    private Transform player;
    private float nextShootTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nextShootTime += Time.deltaTime;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootingRange)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized, shootingRange, playerLayer);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    if (nextShootTime >= shootingInterval)
                    {
                        Shoot();
                        nextShootTime = 0;
                    }
                }
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (player.position - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;
    }
}