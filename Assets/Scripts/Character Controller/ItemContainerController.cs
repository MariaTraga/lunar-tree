using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerController : MonoBehaviour
{
    [SerializeField] GameObject itemContainerUI;
    [SerializeField] GameObject toolbarUI;
    [SerializeField] float maxDistanceToClose = 1f;

    InventoryObject targetItemContainerObject;
    InventoryController inventoryController;
    Transform openedItemContainer;

    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    private void Update()
    {
        if (openedItemContainer != null)
        {
            float distance = Vector2.Distance(openedItemContainer.position, transform.position);
            if (distance > maxDistanceToClose)
            {
                openedItemContainer.GetComponent<Interactable>().Close(GetComponent<Character>());
            }
        }
    }

    public void Open(InventoryObject inventoryObject, Transform _openedItemContainer)
    {
        targetItemContainerObject = inventoryObject;
        itemContainerUI.GetComponentInChildren<ItemContainerPanel>().inventory = targetItemContainerObject;
        if (!targetItemContainerObject) { return; }
        inventoryController.Open();
        itemContainerUI.SetActive(true);
        toolbarUI.SetActive(false);
        openedItemContainer = _openedItemContainer;
    }

    public void Close()
    {
        inventoryController.Close();
        itemContainerUI.SetActive(false);
        toolbarUI.SetActive(true);
        openedItemContainer = null;
    }
}
