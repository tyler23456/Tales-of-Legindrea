using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class ActivatorNode : ActionNode
    {
        [SerializeField] bool isInitiallyActive = true;

        protected override void OnStart()
        {
            state = State.Running;

            if (isInitiallyActive)
            {
                isInitiallyActive = false;
                board.activator.Deactivate();
            }
            else
            {
                isInitiallyActive = true;
                board.activator.Activate();
            }
        }

        protected override State OnUpdate()
        {
            state = State.Success;
            return state;
        }

        protected override void OnStop() { }
    }
}