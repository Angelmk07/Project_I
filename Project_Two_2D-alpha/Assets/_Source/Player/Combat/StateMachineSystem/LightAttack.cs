using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : StateAttack
{
    private readonly Vector2 boxSize = new Vector2(1.5f, 1f);
    private readonly float attackOffset = 0.75f;
    private readonly int damage = 2;
    public override void Start(AttackContext attackContext)
    {
        Vector2 direction = new Vector2(Mathf.Sign(attackContext.TransformAttackTarget.localScale.x), 0);
        Vector2 position = attackContext.TransformAttackTarget.position;
        Vector2 attackPosition = position + direction * attackOffset;

        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPosition, boxSize, 0, attackContext.Layer);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out IDamageable damageble))
            {
                damageble.TakeDamage(damage);
            }
        }
    }
}

