using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Crops System/Default Crop")]
public class CropObject : ScriptableObject
{
    public int timeToGrow = 10;
    public ItemObject yield;
    public int count = 1;

    //Each growth stage sprite that will cycle through as the crop is growing
    public List<Sprite> growthSprites;
    //The time it takes to progress from one stage to the next.
    //The last stage must have the same time as timeToGrow (ex. 10)
    public List<int> growthStageTime;

}
