using ROGUE.Characters;
using UnityEngine;

public class IdleState : State
{
    CharacterController controller;
    Character character;
    CharacterMove move;
    public IdleState(StateEngine engine) : base(engine)
    {
        statename = Statename.Idle;
        Character character = engine.GetComponent<Character> ();
        controller = engine.GetComponent<CharacterController>();
        move = engine.GetComponent<CharacterMove>();
    }

    public override void OnCheckState()
    {
        if(engine.context.GetData("Dead") == 1f) engine.ChangeState(Statename.Dead);
        else if(engine.context.GetData("TakingDamage") == 1f) engine.ChangeState(Statename.TakingDamage);
        else if(controller.attackInput) engine.ChangeState(Statename.Attacking);
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
        // Hero.instance
        move.SetMoveInput(Vector2.zero);
    }

    public override void OnUpdate(float deltaTime)
    {
        OnCheckState();

        if(controller.walkInput != move.isWalking) move.IsWalking(controller.walkInput);
        move.SetMoveInput(controller.moveInput);
    }
}