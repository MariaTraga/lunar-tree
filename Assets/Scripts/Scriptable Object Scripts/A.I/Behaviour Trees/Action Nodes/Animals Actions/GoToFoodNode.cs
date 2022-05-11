using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFoodNode : FollowNode
{
    AnimalController animalController;
    protected override void OnStart()
    {
        animalController = owner.GetComponent<AnimalController>();
        target = animalController.FeederLocation().gameObject;
        Init();
        SetDestination();
    }

    protected override NodeState OnUpdate()
    {
        return base.OnUpdate();
    }

    protected override void OnStop()
    {
        base.OnStop();
    }
}
