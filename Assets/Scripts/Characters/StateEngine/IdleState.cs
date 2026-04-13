using ROGUE.Characters;
using UnityEngine;

public class IdleState : State
{
    CharacterController controller;
    Character character;
    public IdleState(StateEngine engine) : base(engine)
    {
        statename = Statename.Idle;
        Character character = engine.GetComponent<Character> ();
        controller = engine.GetComponent<CharacterController>();
    }

    public override void OnCheckState()
    {
        if(engine.context.GetData("Dead") == 1f) engine.ChangeState(Statename.Dead);
        else if(engine.context.GetData("TakingDamage") == 1f) engine.ChangeState(Statename.TakingDamage);
        else if(controller.attackInput) engine.ChangeState(Statename.Attacking);
    }

    public override void OnEnter()
    {
        Debug.Log("Hello from Idle");
    }

    public override void OnExit()
    {
        Hero.instance.move.SetMoveInput(Vector2.zero);
    }

    public override void OnUpdate(float deltaTime)
    {
        OnCheckState();

        if(controller.walkInput != Hero.instance.move.isWalking) Hero.instance.move.IsWalking(controller.walkInput);
        Hero.instance.move.SetMoveInput(controller.moveInput);
        
    }
}