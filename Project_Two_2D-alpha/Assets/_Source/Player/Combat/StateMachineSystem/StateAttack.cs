using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAttack
{
    private ScriptableObjectAbilities _ability;
    private ScriptableObjectAbilitiesProjectile _abilityProjectile;
    public StateAttack(ScriptableObjectAbilities ability)
    {
        _ability = ability;

    }
    public StateAttack(ScriptableObjectAbilitiesProjectile ability)
    {
        _abilityProjectile = ability;

    }
    public virtual void Start(AttackContext attackContext)
    { }
    public virtual void Exit()
    { }
}

