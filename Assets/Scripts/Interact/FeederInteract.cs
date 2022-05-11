using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederInteract : Interactable
{
    [SerializeField] bool feederOpen = false;
    [SerializeField] AudioClip onOpenAudioClip;
    [SerializeField] public InventoryObject itemContainer;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] string animationString = "act";

    [Header("Feeder Levels")]
    [SerializeField] int level1 = 3;
    [SerializeField] int level2 = 10;
    [SerializeField] int level3 = 20;

    [Header("Feeder Sprites")]
    [SerializeField] Sprite spriteLevel0;
    [SerializeField] Sprite spriteLevel1;
    [SerializeField] Sprite spriteLevel2;
    [SerializeField] Sprite spriteLevel3;


    

    private void Start()
    {
        Debug.Log(gameObject.name);
        HandleFeederLevels();
    }

    public override string Interact(Character character)
    {
        if (feederOpen == false)
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
        feederOpen = true;
        AudioManager.Instance.Play(onOpenAudioClip);
    }

    public override void Close(Character character)
    {
        character.GetComponent<ItemContainerController>().Close();
        feederOpen = false;
        HandleFeederLevels();
        AudioManager.Instance.Play(onOpenAudioClip);
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

    public void HandleFeederLevels()
    {
        foreach(InventorySlot inventorySlot in itemContainer.InventorySlots)
        {
            if (inventorySlot.ItemStack >= level3)
            {
                spriteRenderer.sprite = spriteLevel3;
                return;
            }
            else if (inventorySlot.ItemStack >= level2 && inventorySlot.ItemStack < level3)
            {
                spriteRenderer.sprite = spriteLevel2;
                return;
            }
            else if (inventorySlot.ItemStack >= level1 && inventorySlot.ItemStack < level2)
            {
                spriteRenderer.sprite = spriteLevel1;
                return;
            }
            else
            {
                spriteRenderer.sprite = spriteLevel0;
                return;
            }
        }
    }
}
