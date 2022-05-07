using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    int currentChild;

    protected override void OnStart()
    {
        currentChild = 0;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        Node childNode = childrenNodes[currentChild];
        switch (childNode.Update())
        {
            case NodeState.RUNNING:
                return NodeState.RUNNING;
            case NodeState.SUCCESS:
                //As soon as a child succeeds then the selector returns SUCCESS
                return NodeState.SUCCESS;
            case NodeState.FAILURE:
                //If a child fails, move on to the next child
                currentChild++;
                break;
        }

        //If the current child is the last one then return FAILURE differently continue RUNNING
        return currentChild == childrenNodes.Count ? NodeState.FAILURE : NodeState.RUNNING;
    }
}
