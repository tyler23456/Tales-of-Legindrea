using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Player
{
    public class IsHitNode : DecoratorNode
    {
        protected override void OnStart()
        {
            state = State.Running;
        }

        protected override State OnUpdate()
        {
            if (board.user.getStatFXAnchor.Contains("IsHit"))
                return State.Success;
            else
                return State.Failure;
        }

        protected override void OnStop() { }
    }
}