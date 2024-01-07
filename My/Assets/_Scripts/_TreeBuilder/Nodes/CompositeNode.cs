using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : UserNode
{
    public override UserNode Clone()
    {
        CompositeNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }
}
