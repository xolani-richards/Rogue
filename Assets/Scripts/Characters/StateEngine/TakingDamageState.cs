using ROGUE.Characters;
using UnityEngine;

public class TakingDamageState : State
{
    Character character;
    float countDown;
    public TakingDamageState(StateEngine engine) : base(engine)
    {
        statename = Statename.TakingDamage;
        character = engine.GetComponent<Character>();
    }

    public override void OnCheckState()
    {
        if(engine.context.GetData("Dead") == 1f) engine.ChangeState(Statename.Dead);
    }

    public override void OnEnter()
    {
        countDown = character.damageAnim.length;
        character.canMove = false;
        character.canRotate = false;
        character.animationSystem.PlayOneShot(character.damageAnim);
    }

    public override void OnExit()
    {
        character.canMove = true;
        character.canRotate = true;
        engine.context.isTakingDamage = false;
    }

    public override void OnUpdate(float deltaTime)
    {
        OnCheckState();
        countDown -= deltaTime;
        if (countDown <= 0f) engine.ChangeState(Statename.Idle);
    }
}