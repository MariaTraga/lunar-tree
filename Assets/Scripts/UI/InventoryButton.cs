using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image itemImage;
    [SerializeField] Text itemStackText;
    [SerializeField] Image highlightImage;

    public int slotIndex;

    // cheeki breeki
    TooltipActivator tooltipActivator;

    private void Awake()
    {
        tooltipActivator = GetComponent<TooltipActivator>();
    }
    //

    //Sets the list(inventory) index to a specific button
    public void SetIndex(int index)
    {
        slotIndex = index;
    }

    //Sets the inventory slot item to the button
    public void SetInventoryItem(InventorySlot slot)
    {
        itemImage.sprite = slot.Item.ItemImage;
        itemImage.gameObject.SetActive(true);

        //Show text if item is stackable
        if (slot.Item.Stackable)
        {
            itemStackText.text = slot.ItemStack.ToString();
            itemStackText.gameObject.SetActive(true);
        }
        else
        {
            itemStackText.gameObject.SetActive(false);
        }

        // cheeki breeki

        if (!tooltipActivator)
            tooltipActivator = GetComponent<TooltipActivator>();
        tooltipActivator.description = slot.Item.Name;
        //
    }

    //Clean the button from the image and text
    public void CleanInventoryItem()
    {
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        itemStackText.gameObject.SetActive(false);

    }
    
    //On click method of the button (interface implementation)
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(slotIndex);
    }

    public void Highlight(bool highlight)
    {
        if (highlightImage == null)
        {
            return;
        }
        highlightImage.gameObject.SetActive(highlight);
    }
}
