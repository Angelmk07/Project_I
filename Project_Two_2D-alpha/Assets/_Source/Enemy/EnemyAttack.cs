using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyStatistics enemyStatistics;
    [SerializeField] private float radiusAttack;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayer;

    private bool attackReady = true;

    private void Update()
    {
        if (enemyStatistics != null && enemyStatistics._AiState == EnemyStatistics.AiState.Attack)
        {
            StartCoroutine(TimerAttack());
        }
    }

    private IEnumerator TimerAttack()
    {
        attackReady = false;
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, radiusAttack, playerLayer);
        hitCollider.GetComponent<PlayerStatistics>().TakeDamage(damage);
        yield return new WaitForSeconds(1);
        yield return attackReady = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }
}