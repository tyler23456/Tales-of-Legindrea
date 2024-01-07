using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    [SerializeField] int repetitions = 2;
    int index;

    protected override void OnStart()
    {
        state = State.Running;
        index = 0;
    }

    protected override State OnUpdate()
    {
        if (children[0].Update() == State.Success)
            index++;

        if (index == repetitions)
            state = State.Success;

        return state;
    }

    protected override void OnStop() { }
}
