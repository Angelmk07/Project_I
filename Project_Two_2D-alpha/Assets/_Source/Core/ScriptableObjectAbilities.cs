using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(fileName = "AbilitySettings",menuName = "Ability")]
public class ScriptableObjectAbilities : ScriptableObject
{
    [field: SerializeField] public BoxCollider2D AttackBox {  get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public VisualEffect AttackEffect { get; private set; }
}
