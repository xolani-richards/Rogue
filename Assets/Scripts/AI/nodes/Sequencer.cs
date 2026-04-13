using UnityEngine;

public class Sequencer : CompositeNode
{
    public Sequencer(string name) 
    {
        displayName = name;
    }

    public override void OnEnter()
    {
        if(children.Count == 0) return;
        children[currentChild].OnEnter();
    }

    public override void OnExit()
    {
        if(children.Count == 0) return;
        children[currentChild].OnExit();
        currentChild = 0;
    }

    public override Status Process(float deltaTime)
    {
        if(children.Count == 0) return Status.FAILED;
        Status status = children[currentChild].Process(deltaTime);

        if(status == Status.SUCCESS)
        {
            children[currentChild].OnExit();
            if(currentChild +1 >= children.Count) return Status.SUCCESS;
            currentChild += 1;
            children[currentChild].OnEnter();
            return Status.RUNNING;
        }

        return status;
    }
}