using UnityEngine;

public class InputListner : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private KeyCode buttonJump = KeyCode.Space;
    [SerializeField] private KeyCode buttonRun = KeyCode.LeftShift;
    [SerializeField] private KeyCode buttonAttackLight = KeyCode.Mouse0;
    [SerializeField] private KeyCode buttonAttackHeavy = KeyCode.Mouse1;
    [SerializeField] private KeyCode buttonDropDown = KeyCode.G;
    [SerializeField] private KeyCode buttonDash = KeyCode.F;
    [SerializeField] private KeyCode buttonTakeStack = KeyCode.Q;
    [SerializeField] private KeyCode buttonTp = KeyCode.E;

    private StateMachine _stateMachine;
    private PoisonEffect _poisonEffect;

    public void Construct(StateMachine stateMachine,PoisonEffect poisonEffect)
    {
        _stateMachine = stateMachine;
        _poisonEffect = poisonEffect;
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
        if (Input.GetKeyDown(buttonTakeStack))
        {
            _poisonEffect.TakeAll();
        }
        if (Input.GetKeyDown(buttonTp))
        {
            _poisonEffect.TeleportToInfected();
        }
    }
}