using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
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
                return State.Failure;
            case State.Success:
                childIndex++;
                break;
        }

        return childIndex == children.Count ? State.Success : State.Running;
    }

    protected override void OnStop() { }
}
