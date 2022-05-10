using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; //singleton

    public static GameManager Instance { get {return _instance;} }

    private void Awake()
    {
        if(_instance!=null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject player;
    public InventoryObject inventory;
    public ItemDragDropController dragDropController;
    public DayNightCycle timeController;
    public DialogueController dialogueController;
    public Volume globalVolume;
    public CropsController cropsController;
    public Tooltip tooltip;
    public ShopController shopController;
}
