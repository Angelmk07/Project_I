using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private int damage = 3;
    [SerializeField] private LayerMask affectedLayers;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            CheckAffectedTargets();
            gameObject.SetActive(false);
        }
    }

    private void CheckAffectedTargets()
    {
        Collider2D[] affectedObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);
        foreach (Collider2D obj in affectedObjects)
        {
            if (((1 << obj.gameObject.layer) & affectedLayers) != 0)
            {
                if(obj.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}