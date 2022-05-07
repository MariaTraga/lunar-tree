using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        //Call of the on click method in the dragdropcontroller by passing the inventory slot in the specific button
        GameManager.Instance.dragDropController.OnClick(inventory.InventorySlots[id]);
        //Refresh the panel when a button is clicked
        UpdateDisplay();
    }
}
