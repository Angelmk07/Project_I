using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private InputListner inputListner;
    [SerializeField] private PlayerStatistics playerStatistics;
    [SerializeField] private PoisonEffect poisonEffect;
    [SerializeField] private LayerMask enemyLayer;
    private AttackContext attackContext;
    private LightAttack _lightAttack = new();
    private HeavyAttack _heavyAttack = new();
    private DropDownAttack _dropDownAttack = new();
    private void Awake()
    {
        attackContext = new AttackContext(playerStatistics.transform, enemyLayer, poisonEffect);
        stateMachine = new StateMachine(_lightAttack, _heavyAttack, _dropDownAttack, attackContext);
        inputListner.Construct(stateMachine);
    }
    
}
