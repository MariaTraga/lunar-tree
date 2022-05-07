using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarPanel : ItemPanel
{
    [SerializeField] ToolbarController toolbarController;

    int currentSelectedSlot;

    private void Start()
    {
        Init();
        UpdateHighlight(0);
        toolbarController.onChange += UpdateHighlight; //method UpdateHighlight gets added to the delegate
    }

    public override void OnClick(int id)
    {
        toolbarController.Set(id);
        UpdateHighlight(id);
    }

    public void UpdateHighlight(int id)
    {
        inventoryButtons[currentSelectedSlot].Highlight(false);
        currentSelectedSlot = id;
        inventoryButtons[currentSelectedSlot].Highlight(true);
    }

}
