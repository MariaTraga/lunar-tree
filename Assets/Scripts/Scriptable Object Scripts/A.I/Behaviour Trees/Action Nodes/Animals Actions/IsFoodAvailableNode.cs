using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFoodAvailableNode : ActionNode
{
    [SerializeField] bool isFoodAvailable = false;

    AnimalController animalController;

    protected override void OnStart()
    {
        if (!owner) { return; }
        animalController = owner.GetComponent<AnimalController>();
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeState OnUpdate()
    {
        if (animalController.IsFoodAvailable(out Transform feeder))
        {
            isFoodAvailable = true;
            return NodeState.SUCCESS;
        }
        isFoodAvailable = false;
        return NodeState.FAILURE;
    }
}
