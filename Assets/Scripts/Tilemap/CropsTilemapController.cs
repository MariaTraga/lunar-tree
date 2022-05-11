using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsTilemapController : TimeAgent
{
    [SerializeField] CropContainerObject container;

    //[SerializeField] TileBase seedsTile;
    [SerializeField] TileBase waterTile;

    [SerializeField] private Tilemap targetCropsTilemap;
    [SerializeField] private Tilemap targetWaterTilemap;

    public static Tilemap TargetCropsTilemap { get; private set; }
    public static Tilemap TargetWaterTilemap { get; private set; }


    [SerializeField] GameObject cropsSpritePrefab;



    private void Awake()
    {
        if (!IsTilemapAvailable())
        {
            return;
        }
        CropsTilemapController.TargetCropsTilemap = targetCropsTilemap;
        CropsTilemapController.TargetWaterTilemap = targetWaterTilemap;
    }

    private void Start()
    {
        //report from tilemapCropsController to cropsController
        GameManager.Instance.cropsController.cropsTilemapController = this;
        onTimeTick += Tick;
        Init();
        VisualizeMap();
    }

    private void OnDestroy()
    {
        foreach (CropTile cropTile in container.crops)
        {
            cropTile.renderer = null;
        }
    }

    public void VisualizeMap()
    {
        foreach(CropTile cropTile in container.crops)
        {
            VisualizeTile(cropTile);
        }
    }

    public void Tick()
    {
        if(container.crops == null)
        {
            return;
        }
        foreach (CropTile cropTile in container.crops)
        {
            if (IsTilemapAvailable()) { return; }

            //If crop tile is empty/null then move to the next iteration
            if (cropTile.crop == null) { continue; }
            //If the crop tile that holds a seed (/not grown crop) has not been watered
            //then move to the next iteration
            if (!cropTile.watered ) { continue; }
            //If the crop tile has finished growing then move to the next iteration
            if (cropTile.Completed) { continue; }

            //With each Tick (which hapens every 900 sec / 15 ingame min and is handled by DayNightCycle script)
            //the grow timer increments by 1
            cropTile.growTimer += 1;

            MoveGrowthToNextStage(cropTile);
        }
    }

    //Check availability of tilemaps in case of indoors areas/scenes
    private bool IsTilemapAvailable()
    {
        if (targetCropsTilemap == null || targetWaterTilemap == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static void MoveGrowthToNextStage(CropTile cropTile)
    {
        //When the crop tile timer reaches the time where the crop grows to next stage
        //by checking the list with index growStage (starts at 0) then move to next growth stage
        if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
        {
            TargetCropsTilemap.SetTile(cropTile.position, null);

            //Activate and set sprite renderer if the renderer has not been initialized
            if (cropTile.renderer != null)
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.growthSprites[cropTile.growStage];
            }
            cropTile.growStage += 1;
        }
    }

    public void VisualizeTile(CropTile cropTile)
    {
        targetCropsTilemap.SetTile(cropTile.position, cropTile.crop != null ? cropTile.crop.seedSprite:null);
        targetWaterTilemap.SetTile(cropTile.position, cropTile.watered == true ? waterTile : null);

        if(cropTile.renderer == null)
        {
            GameObject cropGrowthSprite = Instantiate(cropsSpritePrefab,transform);
            cropGrowthSprite.transform.position = targetCropsTilemap.CellToWorld(cropTile.position);
            //cropGrowthSprite.SetActive(false);
            cropTile.renderer = cropGrowthSprite.GetComponent<SpriteRenderer>();
        }

        cropTile.renderer.sortingLayerName = targetCropsTilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        cropTile.renderer.sortingOrder = targetCropsTilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;
        bool growing = 
            cropTile.crop != null && 
            cropTile.growTimer >= cropTile.crop.growthStageTime[0];

        cropTile.renderer.gameObject.SetActive(growing);

        if(growing == true)
        {
            cropTile.renderer.sprite = cropTile.crop.growthSprites[cropTile.growStage - 1];
        }     
    }

    public void PlantSeed(Vector3Int position, CropObject cropToSeed)
    {
        if (!IsTilemapAvailable()) { return; }

        /*//If the tile is already seeded (the position already exists as a key in the dictionary) then return
        if (CheckIfPlantedTile(position))
        {
            return;
        }*/

        if (CheckIfPlantedTile(position)) { return; }

        CropTile tile = new CropTile();
        tile.crop = cropToSeed;
        tile.position = position;

        container.AddCropTile(tile);
        Debug.Log("1"+tile.crop);

        targetCropsTilemap.SetTile(position, cropToSeed.seedSprite);
        
        Debug.Log("2" + container.GetCropTile(position));
        Debug.Log("3" + tile.crop);
        
    }

    /*private void CreatePlantedTile(Vector3Int position, CropObject cropToSeed)
    {
        //Create a new cropTile and add it to the crops dictionary
        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        //After it is added instantiate the sprite that will cycle through the growth of the crop
        //InstantiateCropGrowthSprite(position);

        targetCropsTilemap.SetTile(position, seedsTile);
        crops[(Vector2Int)position].crop = cropToSeed;
    }*/

    public bool CheckIfPlantedTile(Vector3Int position)
    {
        if (container.GetCropTile(position) != null)
        {
            Debug.Log("not empty");
            return true;
        }
        return false;
    }

    public void WaterTile(Vector3Int position)
    {
        if (!IsTilemapAvailable()) { return; }

        /*CropTile tile = container.GetCropTile(position);
        if (tile == null) { return; }*/
        /*if (!CheckIfPlantedTile(position)) { return; }*/

        CropTile tile = container.GetCropTile(position);
        if (tile == null) { return; }
        targetWaterTilemap.SetTile(position, waterTile);
        tile.watered = true;
        VisualizeTile(tile);
    }

    /*private void InstantiateCropGrowthSprite(Vector3Int position)
    {
        GameObject cropGrowthSprite = Instantiate(cropsSpritePrefab);
        cropGrowthSprite.transform.position = targetCropsTilemap.CellToWorld(position);
        cropGrowthSprite.SetActive(false);

        CropTile tile = container.GetCropTile(position);
        tile.renderer = cropGrowthSprite.GetComponent<SpriteRenderer>();
    }*/

    public void PickUpTile(Vector3Int position)
    {
        if (!IsTilemapAvailable()) { return; }

        if (!CheckIfPlantedTile(position)) { return; }

        CropTile tile = container.GetCropTile(position);

        if (tile.Completed)
        {
            ItemSpawnManager.Instance.SpawnItem(position, tile.crop.yield, tile.crop.count);

            tile.Harvested();
            ResetTileState(position);
            VisualizeTile(tile);    
        }

    }

    public void ResetTileState(Vector3Int position)
    {
        targetCropsTilemap.SetTile(position, null);
        targetWaterTilemap.SetTile(position, null);
        container.RemoveCropTile(position);
    }
}
