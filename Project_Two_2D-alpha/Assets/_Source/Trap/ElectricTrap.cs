using System.Collections;
using UnityEngine;

public class ElectricTrap : MonoBehaviour
{
    public int damageAmount;
    public float delay;

    private bool playerInTrigger;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStatistics>() != null)
        {
            playerInTrigger = true;
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamageOverTime(collision.gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStatistics>() != null)
        {
            playerInTrigger = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamageOverTime(GameObject player)
    {
        while (playerInTrigger)
        {
            PlayerStatistics playerStatistics = player.GetComponent<PlayerStatistics>();
            if (playerStatistics != null)
            {
                playerStatistics.TakeDamage(damageAmount);
            }
            yield return new WaitForSeconds(delay);
        }
    }
}