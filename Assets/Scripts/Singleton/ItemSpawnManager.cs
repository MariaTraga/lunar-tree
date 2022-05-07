using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    private static ItemSpawnManager _instance; //singleton

    public static ItemSpawnManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] GameObject pickUpItemPrefab;

    public void SpawnItem(Vector3 itemPosition, ItemObject item, int count)
    {
        //Instantiate an item, that is a copy of the pickUpItemPrefab in the specified position
        GameObject spawnedItem = Instantiate(pickUpItemPrefab, itemPosition, Quaternion.identity);
        //Set the item that should be instantiated and the stack count
        spawnedItem.GetComponent<PickUpItem>().Set(item, count);
    }
}
