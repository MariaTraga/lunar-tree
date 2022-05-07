using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragDropController : MonoBehaviour
{
    [Header("Debug Variables")]
    [SerializeField] public InventorySlot inventorySlot;
    [SerializeField] GameObject dragDropImage;
    
    RectTransform imageTransform;
    Image itemImage;

    void Start()
    {
        //Variable to hold the copy of the inventory slot
        inventorySlot = new InventorySlot();
        //Copy the position/size/etc of the rectangle from the inventory slot and the respective image
        imageTransform = dragDropImage.GetComponent<RectTransform>();
        itemImage = dragDropImage.GetComponent<Image>();
    }

    private void Update()
    {
        SetDragDropImagePosition();
    }

    public void OnClick(InventorySlot inventorySlot)
    {
        //if the pointer copy of the inventory slot is empty (nothing has been coppied/picked up)
        //then create a copy and clear the inventory slot in the button
        if (this.inventorySlot.Item == null)
        {
            this.inventorySlot.Copy(inventorySlot);
            inventorySlot.Clear();
        }
        //if a copy already exists in the pointer
        else
        {
            //Create an item object and the stack count for the coppied item in the pointer
            ItemObject item = this.inventorySlot.Item;
            int count = this.inventorySlot.ItemStack;

            //If the pointer holds the same item as the inventory slot and it is stackable then add them together
            if (this.inventorySlot.Item == inventorySlot.Item && this.inventorySlot.Item.Stackable)
            {
                inventorySlot.Set(item, inventorySlot.ItemStack + count);
                this.inventorySlot.Clear();
                dragDropImage.SetActive(false);
            }
            else
            {
                //Create a copy in the pointer (overwriting the previous one)
                //from the inventory slot still in the inventory (from the button)
                this.inventorySlot.Copy(inventorySlot);
                //Set the variables saved above in the button inventory slot
                inventorySlot.Set(item, count);

            }

            //Note: If the button inventory slot is empty then the coppied item will also be empty
        }

        UpdateDragDropImage();
    }


    //Enable/Disable the image shown on pointer
    private void UpdateDragDropImage()
    {
        if (inventorySlot.Item == null)
        {
            dragDropImage.SetActive(false);
        }
        else
        {
            dragDropImage.SetActive(true);
            itemImage.sprite = inventorySlot.Item.ItemImage;
        }
    }

    private void SetDragDropImagePosition()
    {
        if (dragDropImage.activeInHierarchy)
        {
            //the coppied item rectangle/slot will be shown on the users mouse position
            imageTransform.position = Input.mousePosition;
            DropItemOnWorld();
        }
    }

    private void DropItemOnWorld()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Checks if the pointer is over a gameobject (and not a canvas item)
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Debug.Log("Clicking outside of UI panel");
                //Get world position from screen space
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;

                //Spawn the coppied item (that follows the pointer) in the specified world position
                ItemSpawnManager.Instance.SpawnItem(worldPosition, inventorySlot.Item, inventorySlot.ItemStack);

                inventorySlot.Clear();
                dragDropImage.SetActive(false);
            }
        }
    }

    
}
