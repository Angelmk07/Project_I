using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour, IAttack
{
    public void Attack(Transform player, Rigidbody2D rigidbody2D, float speed)
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.fixedDeltaTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidbody2D.rotation = angle;
    }
}