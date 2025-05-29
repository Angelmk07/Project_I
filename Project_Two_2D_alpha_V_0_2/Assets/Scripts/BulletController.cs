using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;
    [SerializeField] private string tagTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagTarget)
        {
            collision.GetComponent<IStatistics>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}