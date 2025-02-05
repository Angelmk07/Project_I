using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContext
{
    public Transform TransformAttackTarget { get; private set; }
    public LayerMask Layer { get; set; }
    public PoisonEffect poisonEffect;
    public AttackContext(Transform transformAttackTarget, LayerMask layer,PoisonEffect poisonEffect)
    {
        this.TransformAttackTarget = transformAttackTarget;
        this.Layer = layer;
        this.poisonEffect = poisonEffect;
    }
}
