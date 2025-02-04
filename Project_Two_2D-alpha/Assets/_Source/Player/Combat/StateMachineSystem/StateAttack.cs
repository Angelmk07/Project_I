using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAttack
{
    public virtual void Start(AttackContext attackContext)
    { }
    public virtual void Exit()
    { }
}

