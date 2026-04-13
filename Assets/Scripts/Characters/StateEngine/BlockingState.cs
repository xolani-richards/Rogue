using UnityEngine;

public class BlockingState : State
{
    public BlockingState(StateEngine engine) : base(engine)
    {
        statename = Statename.Blocking;
    }

    public override void OnCheckState()
    {
        Debug.Log("Blocking Check State");
    }

    public override void OnEnter()
    {
        Debug.Log("Hello from Blocking");
    }

    public override void OnExit()
    {
        Debug.Log("Goodbye from Blocking");
    }

    public override void OnUpdate(float deltaTime)
    {}
}