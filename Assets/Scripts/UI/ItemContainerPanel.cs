using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        if (GameManager.Instance.dragDropController.inventorySlot.Item != null)
        {
            if (!inventory.CheckIfAllowed(GameManager.Instance.dragDropController.inventorySlot.Item))
            {
                Debug.Log("Only " + inventory.allowedItem.Name + " is allowed in this inventory.");
                return;
            }
        }   
        //Call of the on click method in the dragdropcontroller by passing the inventory slot in the specific button
        GameManager.Instance.dragDropController.OnClick(inventory.InventorySlots[id]);
        //Refresh the panel when a button is clicked
        UpdateDisplay();
    }
}
