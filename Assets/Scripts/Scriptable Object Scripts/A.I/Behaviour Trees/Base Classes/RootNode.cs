using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    public Node childNode;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    //Root node passes the OnUpdate function to its child
    protected override NodeState OnUpdate()
    {
        return childNode.Update();
    }

    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.childNode = childNode.Clone();
        return node;
    }
}
