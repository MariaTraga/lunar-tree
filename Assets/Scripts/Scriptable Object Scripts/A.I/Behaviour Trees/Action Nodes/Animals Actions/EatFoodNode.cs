using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodNode : ActionNode
{
    [SerializeField] string feederString = "Feeding Point";
    public bool isEating = false;

    TimeAgent timeAgent;
    AnimalController animalController;
    GameObject feeder;
    Animator animator;

    protected override void OnStart()
    {
        if (!owner) { return; }
        isEating = false;
        timeAgent = owner.GetComponent<TimeAgent>();
        animalController = owner.GetComponent<AnimalController>();
        feeder = animalController.FeederLocation().gameObject;
        animator = owner.GetComponentInChildren<Animator>();

        timeAgent.onTimeTick += IncrementHunger;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        if (animalController.animalObject.GetHunger() <= animalController.animalObject.hungerSaturation)
        {
            timeAgent.onTimeTick -= owner.GetComponent<AnimalController>().DecrementHunger;
            EatFood();
            isEating = true;
            animator.SetBool("isEating", isEating);
            return NodeState.RUNNING;
        }
        else
        {
            timeAgent.onTimeTick -= IncrementHunger;
            isEating = false;
            animator.SetBool("isEating", isEating);
            return NodeState.SUCCESS;
        }

    }

    private void EatFood()
    {
        if (isEating) { return; }

        feeder.GetComponentInParent<FeederInteract>()
            .itemContainer
            .RemoveFromInventory(animalController.animalFood.Item, animalController.animalFood.ItemStack);

        feeder.GetComponentInParent<FeederInteract>()
            .HandleFeederLevels();
    }

    private void IncrementHunger()
    {
        if (animalController.animalObject.GetHunger() <= animalController.animalObject.hungerSaturation)
        {
            animalController.animalObject.IncrementHunger();
        }
    }
}
