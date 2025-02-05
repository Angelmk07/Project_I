using UnityEngine;

public class InputListner : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private KeyCode buttonJump = KeyCode.Space;
    [SerializeField] private KeyCode buttonRun = KeyCode.LeftShift;
    [SerializeField] private KeyCode buttonAttackLight = KeyCode.Mouse0;
    [SerializeField] private KeyCode buttonAttackHeavy = KeyCode.Mouse0;
    [SerializeField] private KeyCode buttonDropDown = KeyCode.Mouse0;
    [SerializeField] private KeyCode buttonDash = KeyCode.F;

    private StateMachine _stateMachine;

    public void Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

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

        if (Input.GetKeyDown(buttonAttackLight))
        {
            _stateMachine.ChangeState<LightAttack>();
        }
        if (Input.GetKeyDown(buttonAttackHeavy))
        {
            _stateMachine.ChangeState<HeavyAttack>();
        }
        if (Input.GetKeyDown(buttonDropDown))
        {
            _stateMachine.ChangeState<DropDownAttack>();
        }

    }
}