using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContainerPanel : ItemPanel
{
    public enum PanelType
    {
        Player,
        Shop
    }

    public PanelType inventoryType;

    public override void OnClick(int id)
    {
        var selectedItem = inventory.InventorySlots[id].Item;

        if (selectedItem == null)
        {
            return;
        }

        Debug.Log(selectedItem.Name);

        switch (inventoryType)
        {
            case PanelType.Player:
                GameManager.Instance.shopController.Sell(selectedItem);
                break;
            case PanelType.Shop:
                GameManager.Instance.shopController.Buy(selectedItem);
                break;
        }

        //Call of the on click method in the dragdropcontroller by passing the inventory slot in the specific button
        //GameManager.Instance.dragDropController.OnClick(inventory.InventorySlots[id]);
        //Refresh the panel when a button is clicked
        UpdateDisplay();
    }
}
