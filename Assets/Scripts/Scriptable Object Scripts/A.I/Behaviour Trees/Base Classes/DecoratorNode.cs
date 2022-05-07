using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorNode : Node
{
    public Node childNode;

    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);
        node.childNode = childNode.Clone();
        return node;
    }
}
