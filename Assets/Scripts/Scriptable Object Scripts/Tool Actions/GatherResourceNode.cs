using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Ore,
    Crops,
    Animals
}

[CreateAssetMenu(fileName ="New Gather Resource Node Object",menuName ="Tool Actions/Gather Resource Node")]
public class GatherResourceNode : ToolActionObject
{
    [SerializeField] float sizeOfInteractableArea = 0.4f;
    //Define which resource nodes a tool action can hit
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;
    [SerializeField] AudioClip audioClip;

    public override bool OnApply(Vector2 worldPoint)
    {
        //Create interactable area collider
        Collider2D[] toolColliders = Physics2D.OverlapCircleAll(worldPoint,sizeOfInteractableArea);

        //Interact with object
        foreach (Collider2D toolCollider in toolColliders)
        {
            //Get component from the collided object
            ToolHit toolHit = toolCollider.GetComponent<ToolHit>();
            if (toolHit != null)
            {
                //if the object (e.g tree) can be hit by checking
                //the node the tool action can hit (e.g. for axe> cut tree> resource node: tree) => canHitNodesOfType
                //with the type of resource node the collided object is => (ResourceNode)toolhit.CanBeHit(...)
                //and then start coroutine (for the delay for animation)
                if (toolHit.CanBeHit(canHitNodesOfType))
                {
                    toolHit.StartCoroutine(toolHit.Hit());
                    AudioManager.Instance.Play(audioClip);
                    return true;
                }
            }
        }
        return false;
    }
}
