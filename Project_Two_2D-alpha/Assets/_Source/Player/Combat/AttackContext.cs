using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContext
{
    public Transform TransformAttackTarget { get; private set; }
    public LayerMask Layer { get; private set; }
    public PoisonEffect PoisonEffect{ get; private set; }
    public GameObject ProjectileStart{ get; private set; }
    public AttackContext(Transform transformAttackTarget, LayerMask layer,PoisonEffect poisonEffect, GameObject projectileStart)
    {
        TransformAttackTarget = transformAttackTarget;
        Layer = layer;
        PoisonEffect = poisonEffect;
        ProjectileStart = projectileStart;

    }
}
