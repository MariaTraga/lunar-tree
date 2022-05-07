using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : DecoratorNode
{
    NodeState _state;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeState OnUpdate()
    {
        
        switch (childNode.Update())
        {
            case NodeState.RUNNING:
                _state = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                _state = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _state = NodeState.SUCCESS;
                break;
        }
        return _state;
    }
}
