using UnityEngine;
using UnityEngine.VFX;

public class AttackSettings : MonoBehaviour
{
    [Header("Light Attack")]
    private BoxCollider2D lightBox;
    [field: SerializeField] public int LightDamage { get; private set; }
    [field: SerializeField] public VisualEffect LightEffect { get; private set; }
    

    [Header("Heavy Attack")]
    private BoxCollider2D heavyBox;
    [field: SerializeField] public int HeavyDamage { get; private set; }
    [field: SerializeField] public VisualEffect HeavyEffect { get; private set; }

    [Header("DropDown Attack")]
    private BoxCollider2D dropBox;
    [field: SerializeField] public int DropDamage { get; private set; }
    [field: SerializeField] public VisualEffect DropEffect { get; private set; }


    private void Awake()
    {
        lightBox = GetComponent<BoxCollider2D>();
        heavyBox = GetComponent<BoxCollider2D>();
        dropBox = GetComponent<BoxCollider2D>();
    }
}
