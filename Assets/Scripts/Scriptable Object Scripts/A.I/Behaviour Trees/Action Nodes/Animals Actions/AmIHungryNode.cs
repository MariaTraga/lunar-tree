using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmIHungryNode : ActionNode
{
    [SerializeField] bool isHungry = false;

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
        if (animalController.animalObject.GetHunger() <= animalController.animalObject.hungerThreshold)
        {
            isHungry = true;
            return NodeState.SUCCESS;
        }
        isHungry = false;
        return NodeState.FAILURE;
    }
}
