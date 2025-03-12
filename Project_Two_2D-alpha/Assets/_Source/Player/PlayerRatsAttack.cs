using UnityEngine;

public class PlayerRatsAttack : MonoBehaviour
{
    [SerializeField] private GameObject rat;

    public void Spawn(bool inputAction)
    {
        if (inputAction)
        {
            Instantiate(rat, transform.position, transform.rotation);
        }
    }
}