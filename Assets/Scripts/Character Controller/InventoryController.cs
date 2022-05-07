using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject toolbarUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryUI.activeInHierarchy)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    public void Open()
    {
        inventoryUI.SetActive(true);
        toolbarUI.SetActive(false);
    }

    public void Close()
    {
        inventoryUI.SetActive(false);
        toolbarUI.SetActive(true);
    }
}
