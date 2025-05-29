using UnityEngine;

public class InputAction : MonoBehaviour
{
    [SerializeField] private GameObject SkillTree;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerMelee playerMelee;
    [SerializeField] private PlayerShoot playerShoot;

    [SerializeField] private KeyCode keyCodeJump = KeyCode.Space;
    [SerializeField] private KeyCode keyCodeDash = KeyCode.LeftShift;
    [SerializeField] private KeyCode keyCodeMelee = KeyCode.Mouse0;
    [SerializeField] private KeyCode keyCodeShoot = KeyCode.Mouse1;
    [SerializeField] private KeyCode keyCodeOpenSkillTree = KeyCode.F1;

    private bool skillTreeOpen;

    private void Update()
    {
        if (playerMovement != null)
        {
            playerMovement.Walk();
            playerMovement.Jump(Input.GetKeyDown(keyCodeJump));
            playerMovement.Dash(Input.GetKeyDown(keyCodeDash));
            playerMovement.WallSlide();
        }

        if (playerMelee != null)
        {
            playerMelee.Melee(Input.GetKeyDown(keyCodeMelee));
        }

        if (playerShoot != null)
        {
            playerShoot.Shoot(Input.GetKeyDown(keyCodeShoot));
        }

        if (SkillTree != null)
        {
            if (Input.GetKeyDown(keyCodeOpenSkillTree))
            {
                skillTreeOpen = !skillTreeOpen;
            }

            SkillTree.SetActive(skillTreeOpen);
        }
    }
}