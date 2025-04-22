using UnityEngine;
public class Enemy : MonoBehaviour
{
    private bool canSeePlayer = true;

    private void Start()
    {
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

    private void OnDestroy()
    {
        if (PlayerInvisibilityManager.Instance != null)
        {
            PlayerInvisibilityManager.Instance.OnPlayerInvisibilityChanged -= HandlePlayerInvisibilityChanged;
        }
    }
}