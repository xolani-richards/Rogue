public class Selector : CompositeNode
{
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

        if(status == Status.FAILED)
        {
            children[currentChild].OnExit();
            if(currentChild +1 >= children.Count) return Status.FAILED;
            currentChild += 1;
            children[currentChild].OnEnter();
            return Status.RUNNING;
        }

        return status;
    }
}