using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [Header("Pick Up Attributes")]
    [SerializeField] float pickUpSpeed = 4f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float despawnTime = 10f;

    ItemObject itemObject;
    int itemCount = 1;

    Transform player;

    public void Set(ItemObject item, int count)
    {
        itemObject = item;
        itemCount = count;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.ItemImage; 
    }

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        DespawnObject();
        PickUp(); 
    }

    private void DespawnObject()
    {
        despawnTime -= Time.deltaTime;
        if (despawnTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PickUp()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, pickUpSpeed * Time.deltaTime);

        if (distance < 0.1f)
        {
            //TODO Should be moved into specified controller, rather than being checked here.
            if(GameManager.Instance.inventory != null)
            {
                GameManager.Instance.inventory.AddToInventory(itemObject, itemCount);
            }
            else
            {
                Debug.LogWarning("No inventory object attached to the Game Manager");
            }
            Destroy(gameObject);
        }
    }

}
