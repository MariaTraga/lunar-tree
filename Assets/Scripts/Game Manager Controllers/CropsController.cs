using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Stores current state of the crop tile
[Serializable]
public class CropTile
{
    public int growTimer;
    public int growStage = 0;

    public CropObject crop;
    public SpriteRenderer renderer;
    public Vector3Int position;

    public bool watered = false;

    public bool Completed
    {
        get
        {
            if(crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    public void Harvested()
    {
        growStage = 0;
        growTimer = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        watered = false;
    }
}

public class CropsController : MonoBehaviour
{
    public CropsTilemapController cropsTilemapController;

    public void PickUpTile(Vector3Int position)
    {
        if(cropsTilemapController == null) 
        {
            Debug.LogWarning("No crops tilemap controllers are referenced in the crops controller.");
            return; 
        }
        cropsTilemapController.PickUpTile(position);
    }

    public bool CheckIfPlantedTile(Vector3Int position)
    {
        if (cropsTilemapController == null)
        {
            Debug.LogWarning("No crops tilemap controllers are referenced in the crops controller.");
            return false;
        }
        return cropsTilemapController.CheckIfPlantedTile(position);
    }

    public void PlantSeed(Vector3Int position, CropObject cropToSeed)
    {
        if (cropsTilemapController == null)
        {
            Debug.LogWarning("No crops tilemap controllers are referenced in the crops controller.");
            return;
        }
        cropsTilemapController.PlantSeed(position, cropToSeed);
    }

    public void WaterTile(Vector3Int position)
    {
        if (cropsTilemapController == null)
        {
            Debug.LogWarning("No crops tilemap controllers are referenced in the crops controller.");
            return;
        }
        cropsTilemapController.WaterTile(position);
    }

}
