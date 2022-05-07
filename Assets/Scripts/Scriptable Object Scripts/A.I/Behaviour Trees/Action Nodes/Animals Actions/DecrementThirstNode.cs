using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecrementThirstNode : ActionNode
{
    TimeAgent timeAgent;

    protected override void OnStart()
    {
        timeAgent = owner.GetComponent<TimeAgent>();
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeState OnUpdate()
    {
        timeAgent.onTimeTick += owner.GetComponent<AnimalController>().DecrementThirst;
        return NodeState.SUCCESS;
    }
}
