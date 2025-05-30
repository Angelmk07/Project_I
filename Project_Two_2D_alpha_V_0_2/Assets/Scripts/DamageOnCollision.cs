using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private bool destroyOnCollision = true;

    private void OnCollisionEnter(Collision collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ProcessCollision(other.gameObject);
    }

    private void ProcessCollision(GameObject other)
    {
        if ((targetLayers.value & (1 << other.layer)) != 0)
        {
            PlayerStatistics health = other.GetComponent<PlayerStatistics>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
            if (destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}