using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tool Actions/PickUp Tile")]
public class PickUpTile : ToolActionObject
{
    [SerializeField] AudioClip audioClip;
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReadController tilemapReadController, ItemObject item)
    {
        GameManager.Instance.cropsController.PickUpTile(gridPosition);

        AudioManager.Instance.Play(audioClip);

        return true;
    }
}
