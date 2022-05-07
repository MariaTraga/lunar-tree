using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActionObject : ScriptableObject
{
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReadController tilemapReadController, ItemObject item)
    {
        //tilemapReadController is needed to specify with which map we are interacting

        Debug.LogWarning("OnApplyToTileMap in not implemented");
        return true;
    }

    public virtual void OnItemUsed(ItemObject item, InventoryObject inventory)
    {
        //To handle items being used and remove them from the inventory (subtract from stack)
    }

}
