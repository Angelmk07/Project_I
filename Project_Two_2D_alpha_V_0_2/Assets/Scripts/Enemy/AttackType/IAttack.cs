using UnityEngine;

public interface IAttack
{
    public void Attack(Transform player, Rigidbody2D rigidbody2D, float speed);
}