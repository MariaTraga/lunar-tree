using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWaterNode : ActionNode
{
    public bool isDrinking = false;

    AnimalController animalController;
    TimeAgent timeAgent;
    Animator animator;

    protected override void OnStart()
    {
        if (!owner) { return; }
        isDrinking = false;
        animalController = owner.GetComponent<AnimalController>();
        timeAgent = owner.GetComponent<TimeAgent>();
        animator = owner.GetComponentInChildren<Animator>();

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
            animator.SetBool("isDrinking", isDrinking);
            return NodeState.RUNNING;
        }
        else
        {
            timeAgent.onTimeTick -= IncrementThirst;
            isDrinking = false;
            animator.SetBool("isDrinking", isDrinking);
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
