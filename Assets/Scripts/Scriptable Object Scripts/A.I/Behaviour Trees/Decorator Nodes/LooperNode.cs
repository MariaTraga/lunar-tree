using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooperNode : DecoratorNode
{
    [SerializeField] int loopTimes = 1;
    [SerializeField] bool loopOnSuccess = true;
    [SerializeField] bool loopOnFailure = false;

    private int loopCount;

    protected override void OnStart()
    {
        loopCount = 0;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        if (childNode.state == NodeState.RUNNING)
        {
            childNode.Update();
            return NodeState.RUNNING;
        }

        if ((childNode.state == NodeState.FAILURE && loopOnFailure) || (childNode.state == NodeState.SUCCESS && loopOnSuccess))
        {
            return Loop();
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    private NodeState Loop()
    {
        loopCount++;
        if (loopCount >= loopTimes && loopTimes >= 0)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            // restart node
            childNode.state = NodeState.RUNNING;
            //
            return NodeState.RUNNING;
        }
    }
}