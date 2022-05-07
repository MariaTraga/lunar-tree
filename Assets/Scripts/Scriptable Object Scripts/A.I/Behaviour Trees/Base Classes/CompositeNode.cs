using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    public List<Node> childrenNodes = new List<Node>();

    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.childrenNodes = childrenNodes.ConvertAll(c => c.Clone());
        return node;
    }
}
