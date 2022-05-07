using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWaterNode : ActionNode
{
    AnimalController animalController;
    TimeAgent timeAgent;
    public bool isDrinking = false;
    
    protected override void OnStart()
    {
        if (!owner) { return; }
        isDrinking = false;
        animalController = owner.GetComponent<AnimalController>();
        timeAgent = owner.GetComponent<TimeAgent>();

        timeAgent.onTimeTick += IncrementThirst;
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeState OnUpdate()
    {
        if(animalController.animalObject.GetThirst() <= animalController.animalObject.thirstSaturation)
        {
            timeAgent.onTimeTick -= owner.GetComponent<AnimalController>().DecrementThirst;
            isDrinking = true;
            return NodeState.RUNNING;
        }
        else
        {
            timeAgent.onTimeTick -= IncrementThirst;
            isDrinking = false;
            return NodeState.SUCCESS;
        }
        
    }

    private void IncrementThirst()
    {
        if (animalController.animalObject.GetThirst() <= animalController.animalObject.thirstSaturation)
        {
            animalController.animalObject.IncrementThirst();
        }
    }
}
