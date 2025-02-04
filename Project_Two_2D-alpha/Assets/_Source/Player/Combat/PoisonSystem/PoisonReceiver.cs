using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(IDamageable))]
public class PoisonReceiver : MonoBehaviour
{
    private int _currentStacks = 0;
    private int _maxStacks = 8;
    private float _tickDuration = 1.5f;
    private IDamageable _target;
    [SerializeField] private VisualEffect visualEffect;
    private void Start()
    {
        _target = gameObject.GetComponent<IDamageable>();
    }

    public void AddStack(int damage)
    {
        _currentStacks = Mathf.Min(_maxStacks, _currentStacks + 1);
        visualEffect.SetInt(1, _currentStacks);
        if (!IsInvoking(nameof(ApplyPoison)))
        {
            Invoke(nameof(ApplyPoison), _tickDuration);
        }
    }
    public void StackExplode()
    {

    }
    private void ApplyPoison(int damage)
    {
        if (_currentStacks > 0)
        {
            _target.TakeDamage(_currentStacks);
            _currentStacks--;
            visualEffect.SetInt(1, _currentStacks);
        }
    }

    public int GetCurrentStacks() => _currentStacks;
}
