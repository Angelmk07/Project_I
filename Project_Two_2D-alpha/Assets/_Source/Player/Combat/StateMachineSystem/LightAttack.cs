using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LightAttack : StateAttack
{
    private ScriptableObjectAbilities _ability;
    private VisualEffect effectInstance;
    private BoxCollider2D boxCollider;
    public LightAttack(ScriptableObjectAbilities ability) : base(ability)
    {
        _ability = ability;
    }

    public override void Start(AttackContext attackContext)
    {
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



        if (attackContext.Layer != 0)
        {
            List<Collider2D> hits = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = true;
            filter.SetLayerMask(attackContext.Layer);
            boxCollider.OverlapCollider(filter, hits);
            foreach (Collider2D hit in hits)
            {
                Debug.Log(hit);
                if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(_ability.Damage);

                    if (attackContext.poisonEffect != null && hit.TryGetComponent(out PoisonReceiver poisonReceiver))
                    {
                        attackContext.poisonEffect.ApplyPoisonTo(poisonReceiver);
                    }
                }
            }
        }
    }
}