using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private InputListner inputListner;
    [SerializeField] private PlayerStatistics playerStatistics;
    [SerializeField] private PoisonEffect poisonEffect;
    [SerializeField] private ScriptableObjectAbilities light;
    [SerializeField] private ScriptableObjectAbilities heavy;
    [SerializeField] private ScriptableObjectAbilities dropDown;
    [SerializeField] private LayerMask enemyLayer;
    private AttackContext attackContext;
    private LightAttack _lightAttack;
    private HeavyAttack _heavyAttack;
    private DropDownAttack _dropDownAttack;
    private void Awake()
    {
        _lightAttack = new(light);
        _heavyAttack = new(heavy);
        _dropDownAttack = new(dropDown);
        attackContext = new AttackContext(playerStatistics.transform, enemyLayer, poisonEffect);
        stateMachine = new StateMachine(_lightAttack, _heavyAttack, _dropDownAttack, attackContext);
        inputListner.Construct(stateMachine);
    }
    
}
