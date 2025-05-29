using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public int damage;
    [SerializeField] private float distanceAttack;
    [SerializeField] private LayerMask layerMask;

    public void Melee(bool inputAction)
    {
        if (inputAction)
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, distanceAttack, layerMask);

            if (collider2D.Length != null)
            {
                for (int i = 0; i < collider2D.Length; i++)
                {
                    collider2D[i].GetComponent<EnemyStatistics>().TakeDamage(damage);
                }
            }
        }
    }
}