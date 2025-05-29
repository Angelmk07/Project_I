using UnityEngine;

public class DefualtTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IStatistics>() != null)
        {
            collision.GetComponent<IStatistics>().TakeDamage(1);
        }

        if (collision.GetComponent<PlayerMovement>() != null)
        {
            collision.transform.position = collision.GetComponent<PlayerMovement>().lastPosition;
        }
    }
}