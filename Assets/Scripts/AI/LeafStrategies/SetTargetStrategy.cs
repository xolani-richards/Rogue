using System;
using UnityEngine;

public class SetTargetStrategy : ILeafStrategy
{
    Context context;
    Func<Transform> func;
    Transform target = null;
    public SetTargetStrategy(Context context, Func<Transform> func)
    {
        this.func = func;
        this.context = context;
    }
    public void OnEnter()
    {
        target = func();
    }

    public void OnLeave()
    {
        target = null;
    }

    public Node.Status OnProcess(float deltaTime)
    {
        if(target == null) return Node.Status.FAILED;
        context.target = target;
        return Node.Status.SUCCESS;
    }
}