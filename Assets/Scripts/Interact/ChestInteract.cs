using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : Interactable
{
    [SerializeField] GameObject chestOpened;
    [SerializeField] GameObject chestClosed;
    [SerializeField] bool chestOpen = false;
    [SerializeField] AudioClip onOpenAudioClip;
    [SerializeField] AudioClip onCloseAudioClip;
    [SerializeField] public InventoryObject itemContainer;
    [SerializeField] string animationString = "act";


    public override string Interact(Character character)
    {
        if (chestOpen == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }

        return animationString;
    }

    public override void Open(Character character)
    {
        character.GetComponent<ItemContainerController>().Open(itemContainer, transform);
        chestOpen = true;
        AudioManager.Instance.Play(onOpenAudioClip);

        chestClosed.SetActive(false);
        chestOpened.SetActive(true);  
    }

    public override void Close(Character character)
    {
        character.GetComponent<ItemContainerController>().Close();
        chestOpen = false;
        AudioManager.Instance.Play(onCloseAudioClip);

        chestClosed.SetActive(true);
        chestOpened.SetActive(false);    
    }

    public override void Highlight(bool activeHighlight)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Highlighter"))
            {
                child.gameObject.SetActive(activeHighlight);
            }
        }
    }
}
