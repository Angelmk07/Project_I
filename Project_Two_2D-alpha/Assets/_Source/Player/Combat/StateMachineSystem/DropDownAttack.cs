using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownAttack : StateAttack
{
    private Rigidbody2D rigidbody2D;
    private AttackContext attackContext;
    private bool dropingDown = false;
    private readonly Vector2 boxSize = new Vector2(3.5f, 0.25f);
    private readonly float attackOffset = 1.75f;
    private readonly int damage = 2;
    private readonly float speed = 2;
    public override void Start(AttackContext attackContext)
    {
        if (rigidbody2D is null)
        {
            rigidbody2D = attackContext.TransformAttackTarget.GetComponent<Rigidbody2D>();
        }
        this.attackContext = attackContext;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(-Vector2.up * speed);
        dropingDown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dropingDown&& (attackContext.Layer & (1 << collision.gameObject.layer ))!= 0)
        {
            dropingDown = false;
            PerformAttack(attackContext);
        }
    }

    private void PerformAttack(AttackContext attackContext)
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
