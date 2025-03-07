using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour,IDamageable
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [field:SerializeField]public GameObject ProjectileStart { get; private set; }
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
    //PlayerInvisibilityManager.Instance.IsInvisible = !PlayerInvisibilityManager.Instance.IsInvisible;
}