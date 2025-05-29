using UnityEngine;

public class EnemyShoot : MonoBehaviour, IAttack
{
    [SerializeField] private float shootingInterval; // Интервал между выстрелами
    [SerializeField] private float projectileSpeed; // Скорость снаряда
    [SerializeField] private GameObject projectilePrefab;

    private float shootTimer = 0f;

    public void Attack(Transform player, Rigidbody2D rigidbody2D, float speed)
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootingInterval)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * projectileSpeed;
            shootTimer = 0f;

            Destroy(projectile, 1.2f);
        }

        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}