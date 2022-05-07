using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tool Actions/Water Tile")]
public class WaterTile : ToolActionObject
{
    [SerializeField] AudioClip audioClip;
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReadController tilemapReadController, ItemObject item)
    {
        if (GameManager.Instance.cropsController.CheckIfPlantedTile(gridPosition) == false)
        {
            return false;
        }

        GameManager.Instance.cropsController.WaterTile(gridPosition);

        AudioManager.Instance.Play(audioClip);

        return true;
    }
}
