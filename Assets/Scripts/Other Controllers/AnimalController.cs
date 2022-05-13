using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    [SerializeField] public AnimalObject animalObject;
    [SerializeField] ResourceNode resourceNode;

    [Header("NavMesh Variables")]
    [SerializeField] NavMeshAgent navMeshAgent;

    FeederInteract[] feeders;
    [HideInInspector]public InventorySlot animalFood;
    ParticleSystem particles;
    Animator animator;

    public string description { get => animalObject.GetName(); }

    private void Awake()
    {
        feeders = FindObjectsOfType<FeederInteract>();
    }

    private void Start()
    {
        FixRotationFor2D();
        /*animalObject = GetComponent<AnimalData>();*/
        animalFood = animalObject.GetAnimalFood();
        resourceNode = GetComponent<ResourceNode>();
        feeders = FindObjectsOfType<FeederInteract>();
        particles = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();

        particles.gameObject.SetActive(false);

        HandleResourceHappiness();

        TimeAgent timeAgent = GetComponent<TimeAgent>();
        if (timeAgent == null)
        {
            return;
        }
        timeAgent.onTimeTick += DecrementThirst;
        timeAgent.onTimeTick += DecrementHunger;
        timeAgent.onDayTick += HandleHappiness;
    }

    /*private void FixedUpdate()
    {
        HandleMovementAnimation();
    }*/

    void FixRotationFor2D()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    //Thirst decrement per tick
    public void DecrementThirst()
    {
        if (!animalObject) { return; }
        if (animalObject.GetThirst() > 0)
        {
            animalObject.DecrementThirst();
        }
    }

    //Hunger decrement per tick
    public void DecrementHunger()
    {
        if (!animalObject) { return; }
        if (animalObject.GetHunger() > 0)
        {
            animalObject.DecrementHunger();
        }
    }

    //Eating Functions for A.I.
    public bool IsFoodAvailable(out Transform feederLocation)
    {
        feeders = FindObjectsOfType<FeederInteract>();

        foreach (FeederInteract go in feeders)
        {
            if (go.itemContainer.allowedItem == animalFood.Item)
            {
                if (go.itemContainer.CheckItem(animalFood))
                {
                    feederLocation = go.GetComponent<FeederInteract>().transform.Find("Feeding Point");
                    Debug.Log("found " + go.name);
                    return true;
                }
                Debug.Log("No food in food chest");
                feederLocation = null;
                return false;
            }

        }
        Debug.Log("No food chest available");
        feederLocation = null;
        return false;
    }

    public Transform FeederLocation()
    {
        Transform feederLocation;
        IsFoodAvailable(out feederLocation);
        return feederLocation;
    }

    //Handle particles function for A.I.
    public void HandleParticles(bool activate)
    {
        particles.gameObject.SetActive(activate);
    }

    //Happiness calculation per day
    public float CalculateHappinessFactor()
    {
        if (!animalObject) { return 0f; }
        if (animalObject.GetHunger() < animalObject.hungerThreshold)
        {
            animalObject.hungerEffect = -animalObject.hungerEffect;
        }
        if (animalObject.GetThirst() < animalObject.thirstThreshold)
        {
            animalObject.thirstEffect = -animalObject.thirstEffect;
        }
        if (animalObject.GetAffection() < animalObject.affectionThreshold)
        {
            animalObject.affectionEffect = -animalObject.affectionEffect;
        }

        float factor = animalObject.happinessBaseVariation + 1 * animalObject.hungerEffect + 1 * animalObject.thirstEffect + 1 * animalObject.affectionEffect;
        return factor;
    }

    public void HandleHappiness()
    {
        animalObject.HandleHappiness(CalculateHappinessFactor());
        //animalObject.HandleHappiness(CalculateHappinessFactor());
    }



    public void HandleResourceHappiness()
    {
        if(animalObject.GetHappiness() > animalObject.happinessThreshold)
        {
            resourceNode.item = animalObject.animalHappyResource.Item;
        }
        else
        {
            resourceNode.item = animalObject.animalAngryResource.Item;
        }
    }

}
