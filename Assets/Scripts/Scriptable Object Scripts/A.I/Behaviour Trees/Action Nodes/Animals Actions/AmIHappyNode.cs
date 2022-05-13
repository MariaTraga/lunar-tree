using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmIHappyNode : ActionNode
{
    [SerializeField] bool isHappy = true;

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
        animalController.HandleResourceHappiness();
        if (animalController.animalObject.GetHappiness() > animalController.animalObject.happinessThreshold)
        {
            isHappy = true;
            return NodeState.SUCCESS;
        }
        isHappy = false;
        return NodeState.FAILURE;
    }
}
