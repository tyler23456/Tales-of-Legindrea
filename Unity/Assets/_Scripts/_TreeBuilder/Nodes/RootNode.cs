using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : UserNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return children[0].Update();
    }

    public override UserNode Clone()
    {
        RootNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }
}

