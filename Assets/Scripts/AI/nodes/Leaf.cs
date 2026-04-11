using UnityEngine;

public class Leaf: Node
{
    ILeafStrategy strategy;
    public Leaf(string name, ILeafStrategy strategy)
    {
        this.displayName = name;
        this.strategy = strategy;
    }

    public override void OnEnter() => strategy.OnEnter();
    public override void OnExit() => strategy.OnLeave();
    public override Status Process(float deltaTime) => strategy.OnProcess(deltaTime);
}