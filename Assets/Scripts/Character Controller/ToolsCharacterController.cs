using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    CharacterController2D characterController;
    TilemapReadController tilemapReadController;
    ToolbarController toolbarController;
    Rigidbody2D rb;
    Animator animator;

    [Header("Tilemap References")]
    [SerializeField] MarkerManager markerManager;
    
    /*[SerializeField] TileDataObject plowableTile;*/

    [Header("Tool Interact Area Properties")]
    [SerializeField] float offsetDistance = 0.8f;
    /*[SerializeField] float sizeOfInteractableArea = 0.4f;*/
    [SerializeField] float maxDistanceFromPlayer = 1.5f;

    [Header("Empty Hand Actions")]
    [SerializeField] ToolActionObject onTilePickUp;


    Vector3Int selectedTilePos;
    bool selectableTile;

    private void Awake()
    {
        tilemapReadController = FindObjectOfType<TilemapReadController>();
        rb = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController2D>();
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetTileMapReader();
        SelectTile();
        CanSelectTile();
        PlaceMarker();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (UseToolWorld())
            {
                return;
            }
            UseToolGrid();
        }
    }

    //For cross scene referencing
    private void GetTileMapReader()
    {
        if (tilemapReadController == null)
        {
            tilemapReadController = FindObjectOfType<TilemapReadController>();
        }
    }

    private void CanSelectTile()
    {
        Vector2 playerPos = transform.position;
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectableTile = Vector2.Distance(playerPos, cameraPos) < maxDistanceFromPlayer;
        markerManager.ShowMarker(selectableTile);
    }

    private void SelectTile()
    {
        selectedTilePos = tilemapReadController.GetGridPosition(Input.mousePosition, true);
    }

    private void PlaceMarker()
    {
        markerManager.markedGridCellPos = selectedTilePos; 
    }

    private bool UseToolWorld()
    {
        Vector2 position = rb.position + characterController.lastPosition * offsetDistance;

        ItemObject item = toolbarController.GetItem;
        //if no tool is selected then try to use empty hand to pick up something
        if (item == null)
            return false;        
        if(item.onAction == null)
            return false;

        animator.SetTrigger(item.AnimationTrigger);
        bool isCompleted = item.onAction.OnApply(position);

        if (isCompleted == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventory);
            }
        }

        return isCompleted;
    }

    private void UseToolGrid()
    {
        if (selectableTile)
        {
            ItemObject item = toolbarController.GetItem;
            if (item == null)
            {
                UseHandPickUpTile();
                return;
            }
                
            if (item.onTileMapAction == null)
                return;

            animator.SetTrigger(item.AnimationTrigger);
            bool isCompleted = item.onTileMapAction.OnApplyToTileMap(selectedTilePos, tilemapReadController, item);
            Debug.Log(isCompleted);
            
            if(isCompleted == true)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventory);
                }
            }

        }
    }

    private void UseHandPickUpTile()
    {
        if(onTilePickUp == null) { return; }

        //Item parameter is null because the player will use an empty hand to pick up a tile,
        //and the hand is not an item.
        onTilePickUp.OnApplyToTileMap(selectedTilePos, tilemapReadController, null);

    }

}
