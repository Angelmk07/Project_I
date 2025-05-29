using UnityEngine;

public class EnemyMelee : MonoBehaviour, IAttack
{
    public void Attack(Transform player, Rigidbody2D rigidbody2D, float speed)
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;
        rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.fixedDeltaTime);
    }
}