using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tool Actions/Seed Tile")]
public class SeedTile : ToolActionObject
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] List<TileBase> canSeed;
    [SerializeField] CropContainerObject cropContainerObject;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReadController tilemapReadController, ItemObject item)
    {
        TileBase tileToSeed = tilemapReadController.GetTileBase(gridPosition);
        if (canSeed.Contains(tileToSeed) == false)
        {
            return false;
        }
        if (cropContainerObject.GetCropTile(gridPosition) != null)
        {
            return false;
        }
        GameManager.Instance.cropsController.PlantSeed(gridPosition, item.crop);
        //tilemapReadController.cropsController.PlantSeed(gridPosition,item.crop);

        AudioManager.Instance.Play(audioClip);

        return true;
    }

    public override void OnItemUsed(ItemObject item, InventoryObject inventory)
    {
        inventory.RemoveFromInventory(item);
    }
}
