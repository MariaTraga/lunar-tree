using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create new item object", menuName = "Inventory System/Items/Default Item")]

public class ItemObject : ScriptableObject
{
    public string Name;
    public bool Stackable;
    public Sprite ItemImage;
    //Action that a tool object could have (e.g. CutTree) to interact with the world (e.g. Tree)
    public ToolActionObject onAction;
    //Action that a tool object could have to interact with the tilemap
    public ToolActionObject onTileMapAction;
    //Action that an object could have to subtract from stack
    public ToolActionObject onItemUsed;
    public CropObject crop;
    public string AnimationTrigger;
}
