using UnityEngine;

public class InputListner : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private KeyCode buttonJump = KeyCode.Space;
    [SerializeField] private KeyCode buttonRun = KeyCode.LeftShift;
    [SerializeField] private KeyCode buttonAttack = KeyCode.Mouse0;
    [SerializeField] private KeyCode buttonDash = KeyCode.F;

    void Update()
    {
        if (playerMovement != null)
        {
            playerMovement.Move(Input.GetAxis("Horizontal"));
            playerMovement.Jump(Input.GetKeyDown(buttonJump));
            playerMovement.WallSlide(Input.GetAxis("Horizontal"));
            playerMovement.Run(Input.GetKey(buttonRun));
            playerMovement.Dash(Input.GetKeyDown(buttonDash));
        }

        if (playerAttack != null)
        {
            playerAttack.Attack(Input.GetKeyDown(buttonAttack));
        }

    }
}