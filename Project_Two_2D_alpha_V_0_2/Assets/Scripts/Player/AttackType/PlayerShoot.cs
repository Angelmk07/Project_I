using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float shootingInterval; // Интервал между выстрелами
    [SerializeField] private float projectileSpeed; // Скорость снаряда
    [SerializeField] private GameObject projectilePrefab;

    private float shootTimer = 0f;

    private void Start()
    {

    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Shoot(bool inputAction)
    {
        shootTimer += Time.deltaTime;

        if (inputAction)
        {
            if (shootTimer >= shootingInterval)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = transform.right * projectileSpeed;
                shootTimer = 0f;

                Destroy(projectile, 1.2f);
            }
        }
    }
}