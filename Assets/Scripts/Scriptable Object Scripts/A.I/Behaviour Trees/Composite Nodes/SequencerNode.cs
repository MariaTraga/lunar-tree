using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
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
                //If a child succeeds, move on to the next child
                currentChild++;
                break;
            case NodeState.FAILURE:
                //As soon as a child fails then all the sequence returns FAILURE
                return NodeState.FAILURE;
        }

        //If the current child is the last one then return SUCCESS differently continue RUNNING
        return currentChild == childrenNodes.Count ? NodeState.SUCCESS : NodeState.RUNNING;
    }
}
