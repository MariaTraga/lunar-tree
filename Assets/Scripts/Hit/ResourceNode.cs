using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MY ADDITION !!!!!!!!!
public enum RefreshPoint
{
    Tick,
    Day
}

//ResourceNode gets attached on a interactable prefab (e.g. tree) to identify which resource drops from it as well as various data 
public class ResourceNode : ToolHit {

    [SerializeField] float spread = 0.7f;
    [SerializeField] public ItemObject item;
    [SerializeField] int lootCount = 5;
    [SerializeField] int lootCountInOneDrop = 1;
    
    [SerializeField] ResourceNodeType resourceNodeType;

    [SerializeField] bool shouldDestroy = false;
    [SerializeField] float delayTime = 0.1f;
    [SerializeField] float propabilityToRefreshLoot = 0.5f;
    int currentLootCount = 0;

    [SerializeField] RefreshPoint refreshPoint = RefreshPoint.Tick;

    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        if(timeAgent == null)
        {
            return;
        }

        switch (refreshPoint)
        {
            case RefreshPoint.Tick:
                timeAgent.onTimeTick += RefreshLootRandom;
                break;
            case RefreshPoint.Day:
                timeAgent.onDayTick += RefreshLootDay;
                break;
            default:
                break;
        }

        currentLootCount = lootCount;
    }

    //When Hit interaction coroutine occurs spread loot, spawn them and destroy gameobject with a delay to match animation
    public override IEnumerator Hit()
    {
        
        List<Vector3> lootPositionList = new List<Vector3>();

        while (currentLootCount > 0)
        {
            currentLootCount--;

            Vector3 lootPosition = gameObject.transform.position;

            lootPosition.x += spread * Random.value - spread / 2;
            lootPosition.y += spread * Random.value - spread / 2;
            lootPositionList.Add(lootPosition);

        }

        yield return new WaitForSeconds(delayTime);

        foreach(Vector3 pos in lootPositionList)
        {
            ItemSpawnManager.Instance.SpawnItem(pos, item, lootCountInOneDrop);
        }

        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }

    void RefreshLootDay()
    {
        if (!shouldDestroy)
        {
            currentLootCount = lootCount;
        }
    }

    void RefreshLootRandom()
    {
        if (!shouldDestroy)
        {
            if(UnityEngine.Random.value < propabilityToRefreshLoot)
            {
                currentLootCount = lootCount;
            }
        }
    }

    public override bool CanBeHit(List<ResourceNodeType> resourceNodeTypes)
    {
        return resourceNodeTypes.Contains(resourceNodeType);
    }
}
