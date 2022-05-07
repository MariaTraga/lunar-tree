using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public List<InventoryButton> inventoryButtons;
    public InventoryObject inventory;

    private void OnEnable()
    {
        UpdateDisplay();
    }

    private void Start()
    {
        Init();
    }

    private void LateUpdate()
    {
        if (inventory.changed)
        {
            UpdateDisplay();
            inventory.changed = false;
        }
    }

    public void Init()
    {
        UpdateDisplay();
        SetIndexToButtons();
    }

    //Set an index to each button based on the inventory list index
    private void SetIndexToButtons()
    {
        if (inventory == null)
        {
            return;
        }
        for (int i = 0; i < inventory.InventorySlots.Count && i < inventoryButtons.Count; i++)
        {
            inventoryButtons[i].SetIndex(i);
        }
    }

    public virtual void UpdateDisplay()
    {
        if (inventory == null)
        {
            return;
        }
        for (int i = 0; i < inventory.InventorySlots.Count && i < inventoryButtons.Count; i++)
        {
            //if there is no item in the inventory list clean the leftover image and text in each button
            if (inventory.InventorySlots[i].Item == null)
            {
                inventoryButtons[i].CleanInventoryItem();
            }
            //if an item exists in the inventory list then set the item (show image and text) in the specific position/button index
            else
            {
                inventoryButtons[i].SetInventoryItem(inventory.InventorySlots[i]);
            }
        }
    }

    public virtual void OnClick(int id)
    {

    }
}
