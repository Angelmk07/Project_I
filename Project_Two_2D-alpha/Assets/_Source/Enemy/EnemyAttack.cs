using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyStatistics enemyStatistics;
    [SerializeField] private float radiusAttack;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown = 1f; // Cooldown duration

    private bool attackReady = true;

    private void Update()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, radiusAttack, playerLayer);

        if (enemyStatistics != null)
        {
            if (hitCollider != null)
            {
                enemyStatistics._AiState = EnemyStatistics.AiState.Attack;
            }
            else
            {
                enemyStatistics._AiState = EnemyStatistics.AiState.Move;
            }

            if (enemyStatistics._AiState == EnemyStatistics.AiState.Attack && attackReady)
            {
                StartCoroutine(TimerAttack());
            }
        }
    }

    private IEnumerator TimerAttack()
    {
        attackReady = false;

        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, radiusAttack, playerLayer);

        if (hitCollider != null)
        {
            hitCollider.GetComponent<PlayerStatistics>().TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown);

        attackReady = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }
}