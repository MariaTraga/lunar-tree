using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeAgent))]
public class AnimalInteract : Interactable
{ 
    [SerializeField] AudioClip onInteractAudioClip;
    [SerializeField] string playerAnimationTrigger = "act";
    [SerializeField] string emojiAnimationTrigger = "cow-petted";

    AnimalController animalController;
    EmojiController emojiController;
    bool petted = false;

    private void Start()
    {
        animalController = GetComponent<AnimalController>();
        emojiController = GetComponentInChildren<EmojiController>();

        TimeAgent timeAgent = GetComponent<TimeAgent>();
        if (timeAgent == null)
        {
            return;
        }
        timeAgent.onDayTick += HandlePetting;
    }

    void HandlePetting()
    {
        if (!animalController.animalObject.interacted)
        {
            animalController.animalObject.DecrementAffection();
        }
        petted = false;
        animalController.animalObject.HandleAffectionInteraction(petted);
    }

    public override string Interact(Character character)
    {
        if (!animalController.animalObject.interacted)
        {
            petted = true;
            animalController.animalObject.HandleAffectionInteraction(petted);
            animalController.animalObject.IncrementAffection();
            emojiController.AnimateBubble(emojiAnimationTrigger);
            AudioManager.Instance.Play(onInteractAudioClip);
            return playerAnimationTrigger;
        }
        return null;
    }
}
