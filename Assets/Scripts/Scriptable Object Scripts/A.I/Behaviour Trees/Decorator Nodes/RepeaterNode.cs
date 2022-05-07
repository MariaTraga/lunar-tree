using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterNode : DecoratorNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeState OnUpdate()
    {
        //The child always returns running and thus this creates a loop 
        childNode.Update();
        return NodeState.RUNNING;
    }
}
