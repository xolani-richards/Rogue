using System.Collections.Generic;

public abstract class CompositeNode: Node
{
    protected List<Node> children = new ();
    protected int currentChild = 0;
    
    public void AddChild(Node node)
    {
        children.Add(node);
    }
}