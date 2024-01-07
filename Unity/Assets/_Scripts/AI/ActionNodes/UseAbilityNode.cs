using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Player
{
    public class UseAbilityNode : WaitNode
    {
        protected override void OnStart()
        {
            base.OnStart();

            if (board.user.getWeapons.count == 0 || board.target.tag == "PendingDestroy")
                return;

            Blackboard.factory.equipment[board.user.getWeapons.itemName].Use(board.gameObject);
        }

        protected override State OnUpdate()
        {
            state = base.OnUpdate();
            return state;
        }

        protected override void OnStop() 
        {
            base.OnStop();
        }
    }
}