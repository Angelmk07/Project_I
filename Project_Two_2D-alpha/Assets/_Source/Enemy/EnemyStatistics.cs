using UnityEngine;

public class EnemyStatistics : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private bool canSeePlayer = true;
    public enum AiState { Idle, Move, Attack }
    public AiState _AiState;

    private void Start()
    {
        currentHealth = maxHealth;
        PlayerInvisibilityManager.Instance.OnPlayerInvisibilityChanged += HandlePlayerInvisibilityChanged;
        canSeePlayer = !PlayerInvisibilityManager.Instance.IsInvisible; 

    }



    private void HandlePlayerInvisibilityChanged(bool isInvisible)
    {
        canSeePlayer = !isInvisible;

        if (isInvisible)
        {
            Debug.Log(gameObject.name + ": Потерял игрока!");
        }
        else
        {
            Debug.Log(gameObject.name + ": Вижу игрока снова!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (PlayerInvisibilityManager.Instance != null)
        {
            PlayerInvisibilityManager.Instance.OnPlayerInvisibilityChanged -= HandlePlayerInvisibilityChanged;
        }
    }
}