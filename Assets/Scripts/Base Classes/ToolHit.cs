using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    //For tools and resource gathering
    public virtual IEnumerator Hit()
    {
        yield return null;
    }

    public virtual bool CanBeHit(List<ResourceNodeType> resourceNodeTypes)
    {
        return true;
    }
}
