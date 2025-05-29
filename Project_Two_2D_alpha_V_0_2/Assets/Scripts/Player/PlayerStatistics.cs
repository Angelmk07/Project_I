using UnityEngine;
using TMPro;

public class PlayerStatistics : MonoBehaviour, IStatistics
{
    public int money;
    public int health;

    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text healthText;

    void Update()
    {
        moneyText.text = money.ToString();
        healthText.text = health.ToString();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}