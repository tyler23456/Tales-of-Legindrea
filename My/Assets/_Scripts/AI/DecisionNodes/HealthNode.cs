using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Player
{
    public class HealthNode : DecoratorNode
    {
        [SerializeField]
        int criticalHealthThreshold = 25;

        protected override void OnStart()
        {
            state = State.Running;
        }

        protected override State OnUpdate()
        {
            if (board.user.getStats.health <= criticalHealthThreshold)
                return children[0].Update();
            else
                return State.Failure;
        }

        protected override void OnStop() { }
    }
}