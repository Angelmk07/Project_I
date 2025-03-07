using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.VFX;

public class ProjectileLaunchState : StateAttack
{
    private ScriptableObjectAbilitiesProjectile _ability;
    private VisualEffect effectInstance;
    private BoxCollider2D boxCollider;
    private float _lastAttackTime = -Mathf.Infinity;
    private float _attackCooldown = 1.0f;
    public ProjectileLaunchState(ScriptableObjectAbilitiesProjectile ability) : base(ability)
    {
        _ability = ability;
    }

    public override void Start(AttackContext attackContext)
    {
        if (Time.time < _lastAttackTime + _attackCooldown)
        {
            return;
        }

        _lastAttackTime = Time.time;

        float direction = Mathf.Sign(attackContext.TransformAttackTarget.localScale.x);

        if (_ability.AttackEffect != null)
        {
            if (effectInstance == null)
            {
                effectInstance = MonoBehaviour.Instantiate(_ability.AttackEffect, attackContext.TransformAttackTarget.position, _ability.AttackEffect.transform.rotation);
                boxCollider = effectInstance.GetComponent<BoxCollider2D>();
            }

            effectInstance.transform.position = attackContext.TransformAttackTarget.position;

            effectInstance.transform.localScale = new Vector3(direction, direction, direction);

            effectInstance.Play();
        }
        Object.Instantiate(_ability.Projectile,attackContext.ProjectileStart.transform.position, _ability.Projectile.transform.rotation);
        if (_ability.Projectile.TryGetComponent( out Rigidbody2D component))
        {
            component.AddForce(attackContext.ProjectileStart.transform.forward*_ability.Speed,ForceMode2D.Impulse);
        }
    

    }
}
