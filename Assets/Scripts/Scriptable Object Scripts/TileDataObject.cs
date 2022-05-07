using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="New tile data object",menuName ="Tilemap/Tile Data")]
public class TileDataObject : ScriptableObject
{
    public List<TileBase> tiles;
    public bool plowable;    
}
