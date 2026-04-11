using System;

public class ConditionStrategy : ILeafStrategy
{
    Func<bool> conditon;

    public ConditionStrategy(Func<bool> conditon) 
    {
        this.conditon = conditon;
    }

    public void OnEnter()
    {}

    public void OnLeave()
    {}

    public Node.Status OnProcess(float deltaTime)
    {
        return conditon() ? Node.Status.SUCCESS: Node.Status.FAILED;
    }
}