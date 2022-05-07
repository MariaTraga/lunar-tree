using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorNode : Node
{
    public Node childNode;

    public override Node Clone(GameObject owner)
    {
        DecoratorNode node = Instantiate(this);
        node.owner = owner;
        node.childNode = childNode.Clone(owner);
        return node;
    }
}
