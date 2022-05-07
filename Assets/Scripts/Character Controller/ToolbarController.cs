using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] GameObject toolbarUI;
    [SerializeField] int toolbarSize = 10;

    public Action<int> onChange; //delegate

    int selectedToolIndex;

    public ItemObject GetItem
    {
        get
        {
            return GameManager.Instance.inventory.InventorySlots[selectedToolIndex].Item;
        }
    }

    private void Update()
    {
        ScrollToolbar();
        
    }

    private void ScrollToolbar()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedToolIndex++;
                selectedToolIndex = (selectedToolIndex >= toolbarSize ? 0 : selectedToolIndex);
            }
            else
            {
                selectedToolIndex--;
                selectedToolIndex = (selectedToolIndex <= 0 ? toolbarSize - 1 : selectedToolIndex);
            }
            //Action gets invoked(UpdateHightlight) by passing the required int (the ? here is the null conditional operator)
            onChange?.Invoke(selectedToolIndex); 
        }
    }

    public void Set(int id)
    {
        selectedToolIndex = id;
    }
}
