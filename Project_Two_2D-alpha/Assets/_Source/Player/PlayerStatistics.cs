using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Debug.Log("Player died.");
        }
    }
}