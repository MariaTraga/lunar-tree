using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmIThirstyNode : ActionNode
{
    [SerializeField] bool isThirsty = false;

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
        if (animalController.animalObject.GetThirst() <= animalController.animalObject.thirstThreshold)
        {
            isThirsty = true;
            return NodeState.SUCCESS;
        }
        isThirsty = false;
        return NodeState.FAILURE;
    }
}
