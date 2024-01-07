using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    protected override void OnStart()
    {
        childIndex = 0;
    }

    protected override State OnUpdate()
    {
        var child = children[childIndex];

        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                childIndex++;
                break;
            case State.Success:
                return State.Success;
        }

        return childIndex == children.Count ? State.Failure : State.Running;
    }

    protected override void OnStop() { }
}
