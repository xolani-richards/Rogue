using ROGUE.Characters;
using UnityEngine;

public class AttackState : State
{
    MeleeAttack meleeAttack;
    CharacterController controller;
    Character character;
    public AttackState(StateEngine engine) : base(engine)
    {
        statename = Statename.Attacking;
        character = engine.GetComponent<Character>();
        meleeAttack = engine.GetComponent<MeleeAttack>();
        controller = engine.GetComponent<CharacterController>();
    }

    public override void OnCheckState()
    {
        Debug.Log("Attacking Check State");
    }

    public override void OnEnter()
    {
        controller.attackInput = false;
        // character.canMove = false;
        // character.canRotate = false;
        meleeAttack.OnAttack();
    }

    public override void OnExit()
    {
        controller.attackInput = false;
        // character.canMove = true;
        // character.canRotate = true;
    }

    public override void OnUpdate(float deltaTime)
    {
       if(controller.attackInput) meleeAttack.OnAttack();
       else if(!meleeAttack.isExecuting) engine.ChangeState(Statename.Idle);
    }
}