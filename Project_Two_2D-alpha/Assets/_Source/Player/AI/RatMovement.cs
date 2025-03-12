using UnityEngine;

public class RatMovement : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float distanceAttack;
    [SerializeField] private Transform start;
    [SerializeField] private LayerMask layerMaskEnemy;
    [SerializeField] private float moveSpeed = 2f;

    private void Update()
    {
        Move();

        CheckAttack();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void CheckAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(start.position, start.right, distanceAttack, layerMaskEnemy);

        if (hit.collider != null)
        {
            EnemyStatistics enemy = hit.collider.GetComponent<EnemyStatistics>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}