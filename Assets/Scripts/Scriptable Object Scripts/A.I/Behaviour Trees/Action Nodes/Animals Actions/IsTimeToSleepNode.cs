using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTimeToSleepNode : ActionNode
{
    [SerializeField] bool shouldSleep = false;

    float hour;
    AnimalController animalController;


    protected override void OnStart()
    {
        hour = GameManager.Instance.timeController.Hours;
        animalController = owner.GetComponent<AnimalController>();
    }

    protected override void OnStop()
    {
       
    }

    protected override NodeState OnUpdate()
    {
        if(hour >= animalController.animalObject.minHourAwake && hour < animalController.animalObject.maxHourAwake)
        {
            shouldSleep = false;
            return NodeState.FAILURE;
        }
        else
        {
            shouldSleep = true;
            return NodeState.SUCCESS;
        }
    }
}
