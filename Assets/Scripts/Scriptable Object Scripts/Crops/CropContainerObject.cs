using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crops System/Default Crops Container")]
public class CropContainerObject : ScriptableObject
{
    public List<CropTile> crops;

    public CropTile GetCropTile(Vector3Int position)
    {
        return crops.Find(x => x.position == position);
    }

    public void AddCropTile(CropTile cropTile)
    {
        crops.Add(cropTile);
    }

    public void RemoveCropTile(Vector3Int position)
    {
        CropTile cropTile = crops.Find(x => x.position == position);
        crops.Remove(cropTile);
    }

}
