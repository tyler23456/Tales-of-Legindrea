using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateNode : WaitNode
{
    [SerializeField] Vector3 eulerAnglesSpeed;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override State OnUpdate()
    {
        state = base.OnUpdate();

        board.user.getRotation.Add(eulerAnglesSpeed * Time.deltaTime);

        return state;
    }

    protected override void OnStop() { }
}
