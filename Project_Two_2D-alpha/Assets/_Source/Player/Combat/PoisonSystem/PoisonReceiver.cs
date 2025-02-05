using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(IDamageable))]
public class PoisonReceiver : MonoBehaviour
{
   [SerializeField, ReadOnly] private int _currentStacks = 0;
    private int _maxStacks = 8;
    private float _tickDuration = 1.5f;
    private IDamageable _target;
    private int _damage;
    private float multiplier =0.5f;
    [SerializeField] private VisualEffect visualEffect;
    private void Start()
    {
        _target = gameObject.GetComponent<IDamageable>();
    }

    public void AddStack(int damage)
    {
        _damage = damage;
        _currentStacks = Mathf.Min(_maxStacks, _currentStacks + 1);
        UpdateVFX();
        if (!IsInvoking(nameof(ApplyPoison)))
        {
            Invoke(nameof(ApplyPoison), _tickDuration);
        }
    }
    public void StackExplode()
    {
        _target.TakeDamage((int)(_currentStacks * _damage* multiplier));
        _currentStacks = 0;
        UpdateVFX();
    }
    private void ApplyPoison()
    {
        if (_currentStacks > 0)
        {
            _target.TakeDamage(_currentStacks* _damage);
            _currentStacks--;
            UpdateVFX();
            Invoke(nameof(ApplyPoison), _tickDuration);
        }
    }
    private void UpdateVFX()
    {
        visualEffect.SetInt("Stack", _currentStacks);
    }
    public int GetCurrentStacks() => _currentStacks;
}
