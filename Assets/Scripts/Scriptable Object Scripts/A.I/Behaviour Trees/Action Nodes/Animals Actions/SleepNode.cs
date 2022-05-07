using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepNode : ActionNode
{
    [SerializeField] bool asleep = false;

    float hour;
    TimeAgent timeAgent;
    Animator animator;
    AnimalController animalController;

    protected override void OnStart()
    {
        if (!owner) { return; }
        hour = GameManager.Instance.timeController.Hours;
        timeAgent = owner.GetComponent<TimeAgent>();
        animator = owner.GetComponentInChildren<Animator>();
        animalController = owner.GetComponent<AnimalController>();

        timeAgent.onTimeTick += CheckTime;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        if (hour >= animalController.animalObject.minHourAwake && hour < animalController.animalObject.maxHourAwake)
        {
            asleep = false;
            animator.SetBool("isSleeping", asleep);
            animalController.HandleParticles(asleep);
            return NodeState.SUCCESS;
        }
        else
        {
            asleep = true;
            animator.SetBool("isSleeping", asleep);
            animalController.HandleParticles(asleep);
            return NodeState.RUNNING;
        }
    }

    private void CheckTime()
    {
        hour = GameManager.Instance.timeController.Hours;
    }
}
