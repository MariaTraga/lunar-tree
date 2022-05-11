using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animal
{
    Undefined,
    Cow,
    Chicken,
    Sheep,
    Bunny
}

[CreateAssetMenu(fileName = "New Animal", menuName = "Animals/Default Animal")]
public class AnimalObject : ScriptableObject
{
    [SerializeField] public Animal animalType;
    [SerializeField] public AudioClip happySound;
    [SerializeField] public AudioClip angrySound;

    [Header("Animal Variables")]
    [SerializeField] string animalName = "Animal";
    [SerializeField] float thirst = 50f;
    [SerializeField] float hunger = 50f;
    [SerializeField] float affection = 85f;
    [SerializeField] float happiness = 100f;
    [SerializeField] InventorySlot animalFood;

    [Header("Animal Hunger Data")]
    [SerializeField] public float hungerDecrement = -1f;
    [SerializeField] public float hungerIncrement = 3f;
    [SerializeField] public float hungerThreshold = 20f;
    [SerializeField] public float hungerSaturation = 50f;

    [Header("Animal Thirst Data")]
    [SerializeField] public float thirstDecrement = -1f;
    [SerializeField] public float thirstIncrement = 3f;
    [SerializeField] public float thirstThreshold = 20f;
    [SerializeField] public float thirstSaturation = 50f;

    [Header("Animal Sleep Data")]
    [SerializeField] public float minHourAwake = 8f;
    [SerializeField] public float maxHourAwake = 22f;

    [Header("Animal Affection Data")]
    [SerializeField] public float affectionThreshold = 80f;
    [SerializeField] public float affectionDecrement = -1f;
    [SerializeField] public float affectionIncrement = 2f;
    [SerializeField] public bool interacted = false;

    [Header("Animal Happiness Data")]
    [SerializeField] public float happinessThreshold = 90f;
    [SerializeField] public float happinessBaseVariation = 5f;
    [SerializeField] public float hungerEffect = 4f;
    [SerializeField] public float thirstEffect = 1f;
    [SerializeField] public float affectionEffect = 3f;

    public string GetName()
    {
        return $"{animalName} ({animalType})";
    }

    public float GetHunger()
    {
        return hunger;
    }

    public float GetThirst()
    {
        return thirst;
    }

    public float GetAffection()
    {
        return affection;
    }

    public float GetHappiness()
    {
        return happiness;
    }

    public InventorySlot GetAnimalFood()
    {
        return animalFood;
    }

    public void IncrementHunger()
    {
        hunger += hungerIncrement;
    }

    public void DecrementHunger()
    {
        hunger += hungerDecrement;
    }

    public void IncrementThirst()
    {
        thirst += thirstIncrement;
    }

    public void DecrementThirst()
    {
        thirst += thirstDecrement;
    }

    public void IncrementAffection()
    {
        affection += affectionIncrement;
    }

    public void DecrementAffection()
    {
        affection += affectionDecrement;
    }

    public void HandleAffectionInteraction(bool hasInteracted)
    {
        interacted = hasInteracted;
    }

    public void HandleHappiness(float factor)
    {
        happiness += factor;
    }
}
