using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private readonly Dictionary<Type, StateAttack> _states;
    public StateAttack CurrentState { get; private set; }
    private AttackContext _attackContext;

    public StateMachine(LightAttack lightAttack, HeavyAttack heavyAttack,DropDownAttack dropDownAttack, AttackContext attackContext)
    {
        CurrentState = lightAttack;
        _attackContext = attackContext;

        _states = new Dictionary<Type, StateAttack>()
        {
            { typeof(LightAttack), lightAttack},
            { typeof(HeavyAttack), heavyAttack},
            { typeof(DropDownAttack), dropDownAttack}
            
        };
    }

    public void Attack()
    {
        if (CurrentState is StateAttack stateAttack)
        {
            stateAttack.Start(_attackContext);
        }
    }

    public void Finish()
    {
        CurrentState.Exit();
    }
    public void ChangeState<T>() where T : StateAttack
    {
        if (_states.ContainsKey(typeof(T)))
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = _states[typeof(T)];
            CurrentState.Start(_attackContext);
        }
    }
    public bool IsCurrentState<T>() where T : StateAttack
    {
        return CurrentState is T;
    }


}