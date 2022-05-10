using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : Interactable
{ 
    [SerializeField] public InventoryObject shopInventory;

    bool shopOpen = false;

    public override string Interact(Character character)
    {
        if (!shopOpen)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }
        return null;
    }

    public override void Open(Character character)
    {
        character.GetComponent<ShopController>().Open(shopInventory,transform);
        shopOpen = true;
    }

    public override void Close(Character character)
    {
        character.GetComponent<ShopController>().Close();
        shopOpen = false;
    }
}
