using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] TileBase markerTile;
    public Vector3Int markedGridCellPos;
    private Vector3Int oldGridCellPos;

    bool markerPlaced;

    private void Update()
    {
        if (!markerPlaced)
        {
            return;
        }
        //Clean old marked tile
        targetTilemap.SetTile(oldGridCellPos, null);
        //Mark new tile
        targetTilemap.SetTile(markedGridCellPos, markerTile);
        oldGridCellPos = markedGridCellPos;
    }

    public void ShowMarker(bool selectableTile)
    {
        markerPlaced = selectableTile;
        targetTilemap.gameObject.SetActive(markerPlaced);
    }
}
