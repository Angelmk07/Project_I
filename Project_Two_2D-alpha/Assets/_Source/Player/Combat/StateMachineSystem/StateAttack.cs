using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAttack
{
    private ScriptableObjectAbilities _ability;
    public StateAttack(ScriptableObjectAbilities ability)
    {
        _ability = ability;

    }
    public virtual void Start(AttackContext attackContext)
    { }
    public virtual void Exit()
    { }
}

