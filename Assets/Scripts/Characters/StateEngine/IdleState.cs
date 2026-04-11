using UnityEngine;

public class IdleState : State
{
    public IdleState(StateEngine engine) : base(engine)
    {
        statename = Statename.Idle;
    }

    public override void OnCheckState()
    {
        Debug.Log("Idle Check State");
    }

    public override void OnEnter()
    {
        Debug.Log("Hello from Idle");
    }

    public override void OnExit()
    {
        Debug.Log("Goodbye from Idle");
    }
}