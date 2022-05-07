using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecrementHungerNode : ActionNode
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
        timeAgent.onTimeTick += owner.GetComponent<AnimalController>().DecrementHunger;
        return NodeState.SUCCESS;
    }
}
