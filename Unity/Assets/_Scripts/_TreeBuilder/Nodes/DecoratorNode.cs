using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorNode : UserNode
{
    public override UserNode Clone()
    {
        DecoratorNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }
}
