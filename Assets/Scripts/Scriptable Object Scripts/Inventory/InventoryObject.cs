using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventorySlot //Slot that shows in inventory with the item and the stack amount
{
    public ItemObject Item;
    public int ItemStack;

    //Creates a copy of the given slot
    public void Copy(InventorySlot slot)
    {
        Item = slot.Item;
        ItemStack = slot.ItemStack;
    }

    //Clears the slot
    public void Clear()
    {
        Item = null;
        ItemStack = 0;
    }

    //Sets the parameters in a slot
    public void Set(ItemObject item, int itemStack)
    {
        Item = item;
        ItemStack = itemStack;
    }

}

[CreateAssetMenu(fileName = "Create new inventory object", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> InventorySlots;
    public bool changed; //variable to help toobar update with each item addition or removal from the top bar of the inventory
    public ItemObject allowedItem;

    public void AddToInventory(ItemObject item, int count = 1)
    {
        changed = true;
        if (item.Stackable)
        {
            //Find slot with the same stackable item
            InventorySlot inventorySlot = InventorySlots.Find(x => x.Item == item);
            if (inventorySlot != null)
            {
                inventorySlot.ItemStack += count;
            }
            else
            {
                //Find empty slot
                inventorySlot = InventorySlots.Find(x => x.Item == null);
                if (inventorySlot != null)
                {
                    inventorySlot.Item = item;
                    inventorySlot.ItemStack = count;
                }
            }
        }
        else
        {
            //Find empty slot in inventory if item is not stackable
            InventorySlot inventorySlot = InventorySlots.Find(x => x.Item == null);
            //If there are available slots item is added to inventory
            if (inventorySlot != null)
            {
                inventorySlot.Item = item;
            }
        }
    }

    public void RemoveFromInventory(ItemObject itemToRemove, int count = 1)
    {
        changed = true;
        if (itemToRemove.Stackable)
        {
            //Find slot with the same stackable item
            InventorySlot inventorySlot = InventorySlots.Find(x => x.Item == itemToRemove);
            if (inventorySlot == null) { return; }

            inventorySlot.ItemStack -= count;
            if (inventorySlot.ItemStack <= 0)
            {
                inventorySlot.Clear();
            }
        }
        else
        {
            while (count > 0)
            {
                count -= 1;
                InventorySlot inventorySlot = InventorySlots.Find(x => x.Item == itemToRemove);
                if (inventorySlot == null) { return; }

                inventorySlot.Clear();
            }
        }
    }

    //Check if there is available free space in the inventory
    public bool CheckFreeSpace()
    {
        foreach (InventorySlot inventorySlot in InventorySlots)
        {
            if (inventorySlot.Item == null)
            {
                return true;
            }
        }

        return false;
    }

    //Check if we have a specific item in the inventory
    //alse check the quantity of the specific item
    public bool CheckItem(InventorySlot checkedSlot)
    {
        InventorySlot inventorySlot = InventorySlots.Find(x => x.Item == checkedSlot.Item);

        if (inventorySlot == null)
        {
            return false;
        }
        if (checkedSlot.Item.Stackable)
        {
            return inventorySlot.ItemStack > checkedSlot.ItemStack;
        }

        return true;
    }

    public bool CheckIfAllowed(ItemObject checkedItem)
    {
        if (allowedItem != null)
        {
            if(allowedItem == checkedItem)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

}
