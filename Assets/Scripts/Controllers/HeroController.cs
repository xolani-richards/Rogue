using UnityEngine;
using ROGUE.Characters;

public class HeroController : CharacterController
{
    [SerializeField] Vector2 moveInput;
    [SerializeField] bool attackInput;
    [SerializeField] bool walkInput;
    PlayerInput inputs;

    void Awake()
    {
        inputs = new ();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        
        inputs.Player.Attack.performed += ctx => attackInput = true;
        inputs.Player.Attack.canceled += ctx => attackInput = false;
        inputs.Player.Sprint.performed += ctx => OnWalk(true);
        inputs.Player.Sprint.canceled += ctx => OnWalk(false);
        inputs.Player.Enable();
    }

    void Update()
    {
        OnMove();
        OnAttack();
    }

    void OnWalk(bool value)
    {
        walkInput = value;
        Hero.instance.move.IsWalking(walkInput);
    }

    void OnMove() => Hero.instance?.move.SetMoveInput(moveInput);


    void OnAttack()
    {
        if(!attackInput) return;
        // Hero.instance?.abilityController.OnExecute();
        Hero.instance?.attack.OnAttack();
        attackInput = false;
    }

    void OnBlock()
    {}

    void OnDodge()
    {}

}
