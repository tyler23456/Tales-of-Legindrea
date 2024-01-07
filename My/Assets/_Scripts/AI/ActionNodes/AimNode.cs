using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GDA.Player
{
    public class AimNode : DecoratorNode
    {
        protected override void OnStart()
        {
            board.user.getAimEvents.Start();
        }

        protected override State OnUpdate()
        {
            board.user.getWeaponAnchors.SetAimAnchorPosition(board.target.getStatFXAnchor.getAnchor.position);
            board.user.getRotation.Forward(board.user.getWeaponAnchors.aimDirection);
            return children[0].Update();
        }

        protected override void OnStop() 
        {
            board.user.getAimEvents.End();
        }
    }
}