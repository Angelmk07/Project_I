using UnityEngine;

public class DefualtTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStatistics>() != null)
        {
            collision.GetComponent<PlayerStatistics>().TakeDamage(1);
        }

        if (collision.GetComponent<PlayerMovement>() != null)
        {
            collision.transform.position = collision.GetComponent<PlayerMovement>().lastPosition;
        }
    }
}