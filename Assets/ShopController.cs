using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] GameObject shopUICanvas;
    [SerializeField] GameObject toolbarUI;
    [SerializeField] float maxDistanceToClose = 3f;

    [SerializeField] AudioClip onOpenAudioClip;
    [SerializeField] AudioClip onCloseAudioClip;
    [SerializeField] AudioClip onBuyAudioClip;
    [SerializeField] AudioClip onSellAudioClip;

    PlayerDataObject playerData;
    InventoryObject targetShopInventoryObject;
    Transform openedShop;
    bool shopOpen = false;

    private void Start()
    {
        playerData = GameManager.Instance.player.GetComponent<Character>().playerData;
    }

    private void Update()
    {
        if(openedShop)
        {
            float distance = Vector2.Distance(transform.position, openedShop.position);
            if (distance > maxDistanceToClose)
            {
                openedShop.GetComponent<Interactable>().Close(GetComponent<Character>());
            }
        }

    }

    public void Open(InventoryObject inventoryObject, Transform _openedShop)
    {
        targetShopInventoryObject = inventoryObject;
        shopUICanvas.GetComponentsInChildren<ShopContainerPanel>().First(p => p.inventoryType == ShopContainerPanel.PanelType.Shop).inventory = targetShopInventoryObject;
        shopUICanvas.SetActive(true);
        toolbarUI.SetActive(false);
        openedShop = _openedShop;
        AudioManager.Instance.Play(onOpenAudioClip);
    }

    public void Close()
    {
        shopUICanvas.SetActive(false);
        toolbarUI.SetActive(true);
        openedShop = null;
        AudioManager.Instance.StopAllAudioSources();
        AudioManager.Instance.Play(onCloseAudioClip);
    }

    public void Buy(ItemObject item)
    {
        if(item.Price > playerData.Money)
        {
            Debug.Log("Not enough money.");
        }
        else
        {
            GameManager.Instance.inventory.AddToInventory(item);
            playerData.Money -= item.Price;
            // play buy sound
            AudioManager.Instance.Play(onBuyAudioClip);
        }
    }
    public void Sell(ItemObject item)
    {
        if (!item.CanSell)
        {
            Debug.Log($"Cannot sell {item.Name}.");
        }
        else
        {
            GameManager.Instance.inventory.RemoveFromInventory(item);
            playerData.Money += Mathf.Max(item.Price / 2, 1);
            // play sell sound
            AudioManager.Instance.Play(onSellAudioClip);
        }
    }
}
