using System;
using UnityEngine;

public class PlayerInvisibilityManager : MonoBehaviour
{
    public static PlayerInvisibilityManager Instance { get; private set; }

    public event Action<bool> OnPlayerInvisibilityChanged; // Событие изменения состояния невидимости

    private bool isInvisible;

    public bool IsInvisible
    {
        get => isInvisible;
        set
        {
            if (isInvisible != value)
            {
                isInvisible = value;
                OnPlayerInvisibilityChanged?.Invoke(isInvisible); // Вызываем событие
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}