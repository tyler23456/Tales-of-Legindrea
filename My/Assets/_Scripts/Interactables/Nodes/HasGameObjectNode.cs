using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class HasGameObjectNode : DecoratorNode
    {
        protected override void OnStart()
        {
            board.hasGameObject.gameObjects.RemoveAll(e => e == null || e.tag == "PendingDestroy");
            if (board.hasGameObject.gameObjects.Count == 0)
                state = State.Running;
            else
                state = State.Failure;
        }

        protected override State OnUpdate()
        {
            if (state == State.Failure || state == State.Success)
                return state;

            return children[0].Update();

        }

        protected override void OnStop() { }
    }
}