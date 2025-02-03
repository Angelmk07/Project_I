using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float radiusAttack;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask enemyLayer;

    private bool attackReady = true;

    public void Attack(bool inputAction)
    {
        if (inputAction && attackReady)
        {
            StartCoroutine(TimerAttack());
        }
    }

    private IEnumerator TimerAttack()
    {
        attackReady = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radiusAttack, enemyLayer);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Debug.Log("Player attack: " + hitColliders[i].name);
                hitColliders[i].GetComponent<EnemyStatistics>().TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(1);
        yield return attackReady = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }
}