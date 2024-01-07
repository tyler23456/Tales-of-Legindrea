using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcurrentNode : CompositeNode
{
    protected override void OnStart()
    {
    }

    protected override State OnUpdate()
    {
        foreach (UserNode node in children)
        {
            state = node.Update();

            if (state == State.Failure || state == State.Success)
                return state;
        }

        return state;
    }

    protected override void OnStop() { }
}
