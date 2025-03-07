
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(fileName = "AbilitySettings", menuName = "AbilityProjectile")]
public class ScriptableObjectAbilitiesProjectile : ScriptableObject
{
    [field: SerializeField] public GameObject Projectile { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int Speed { get; private set; }
    [field: SerializeField] public VisualEffect AttackEffect { get; private set; }
}
