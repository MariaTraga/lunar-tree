using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapReadController : MonoBehaviour
{
    [SerializeField] Tilemap baseTilemap;

    private void Start()
    {
        if (!GetBaseTilemap()){
            return;
        } 
    }

    public bool GetBaseTilemap()
    {
        if (baseTilemap == null)
        {
            baseTilemap = FindObjectOfType<BaseTilemap>().gameObject.GetComponent<Tilemap>();
        }
        if (baseTilemap == null)
        {
            return false;
        }
        return true;
    }

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition = false)
    {
        if (!GetBaseTilemap())
        {
            return Vector3Int.zero;
        }

        Vector3 worldPosition;

        //if a the position variable originates from the mouse input
        if (mousePosition)
        {
            //Get world position from mouse position
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        //Get grid/cell position from world position (from a specific tilemap)
        Vector3Int gridPosition = baseTilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        if (!GetBaseTilemap())
        {
            return null;
        }
        //Get specific tile from the grid position
        TileBase tile = baseTilemap.GetTile(gridPosition);

        return tile;
    }


}
