using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float waitDuration = 1f;
    float startTime;
    protected override void OnStart()
    {
        startTime = Time.time;
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeState OnUpdate()
    {
        if(Time.time - startTime > waitDuration)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;
    }
}
