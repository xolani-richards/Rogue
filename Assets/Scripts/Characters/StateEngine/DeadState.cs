using ROGUE.Characters;
using UnityEngine;

public class DeadState : State
{
    Character character;
    NPCController controller;
    float countDown;
    public DeadState(StateEngine engine) : base(engine)
    {
        statename = Statename.Dead;
        character = engine.GetComponent<Character>();
        controller = engine.GetComponent<NPCController>();
    }

    public override void OnCheckState()
    {
        Debug.Log("Dead Check State");
    }

    public override void OnEnter()
    {
        character.canMove = false;
        character.canRotate = false;
        character.isExecuting = true;
        countDown = character.deathAnim.length - 0.3f;
        character.animationSystem.PlayOneShot(character.deathAnim);
    }

    public override void OnExit()
    {
        Debug.Log("Goodbye from Dead");
    }

    public override void OnUpdate(float deltaTime)
    {
        countDown -= deltaTime;
        if (countDown <= 0) onDead();
    }

    void onDead ()
    {
        character.animator.enabled = false;
        if (controller != null) controller.enabled = false;
        engine.enabled = false;
    }
}