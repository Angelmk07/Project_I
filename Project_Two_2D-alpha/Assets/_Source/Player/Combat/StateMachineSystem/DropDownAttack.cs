using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DropDownAttack : StateAttack
{
    private Rigidbody2D rigidbody2D;
    private ScriptableObjectAbilities _ability;
    private VisualEffect effectInstance;
    private AttackContext _attackContext;
    private BoxCollider2D boxCollider;
    private readonly float speed = 450;
    private bool isdroping;
    private float _lastAttackTime = -Mathf.Infinity;
    private float _attackCooldown = 1.5f;
    public DropDownAttack(ScriptableObjectAbilities ability) : base(ability)
    {
        _ability = ability;
    }

    public override void Start(AttackContext attackContext)
    {
        this._attackContext = attackContext;
        if (rigidbody2D is null)
        {
            rigidbody2D = this._attackContext.TransformAttackTarget.GetComponent<Rigidbody2D>();
        }
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(-Vector2.up * speed);
        isdroping = true;
    }


    public void PerformAttack()
    {
        if (isdroping)
        {
            if (Time.time < _lastAttackTime + _attackCooldown)
            {
                return;
            }

            _lastAttackTime = Time.time;

            if (_ability.AttackEffect != null)
            {
                if (effectInstance == null)
                {
                    effectInstance = MonoBehaviour.Instantiate(_ability.AttackEffect, _attackContext.TransformAttackTarget.position, _ability.AttackEffect.transform.rotation);
                    boxCollider = effectInstance.GetComponent<BoxCollider2D>();
                }

                effectInstance.transform.position = _attackContext.TransformAttackTarget.position;
                effectInstance.Play();
            }



            if (_attackContext.Layer != 0)
            {
                List<Collider2D> hits = new List<Collider2D>();
                ContactFilter2D filter = new ContactFilter2D();
                filter.useTriggers = true;
                filter.SetLayerMask(_attackContext.Layer);
                boxCollider.OverlapCollider(filter, hits);
                foreach (Collider2D hit in hits)
                {
                    Debug.Log(hit);
                    if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.TakeDamage(_ability.Damage);

                        if (_attackContext.poisonEffect != null && hit.TryGetComponent(out PoisonReceiver poisonReceiver))
                        {
                            _attackContext.poisonEffect.ApplyPoisonTo(poisonReceiver);
                        }
                    }
                }
            }
            isdroping = false;
        }
    }
}
