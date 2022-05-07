using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TimeAgent))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] ItemObject itemToSpawn;
    [SerializeField] float spread = 0.7f;
    [SerializeField] int lootCountInOneDrop = 1;
    [SerializeField] float propabilityToSpawn = 0.5f;

    [SerializeField] float delayTime = 0.1f;

    private void Start()
    {
        //ItemSpawner wants to execute the CallSpawn() every time it ticks
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += CallSpawn;
    }

    IEnumerator Spawn()
    {
        if (UnityEngine.Random.value < propabilityToSpawn)
        {
            Vector3 lootPosition = gameObject.transform.position;

            lootPosition.x += spread * Random.value - spread / 2;
            lootPosition.y += spread * Random.value - spread / 2;

            yield return new WaitForSeconds(delayTime);

            ItemSpawnManager.Instance.SpawnItem(lootPosition, itemToSpawn, lootCountInOneDrop);
        }
    }

    void CallSpawn()
    {
        StartCoroutine(Spawn());
    }

}
