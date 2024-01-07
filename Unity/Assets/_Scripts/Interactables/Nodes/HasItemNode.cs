using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class HasItemNode : DecoratorNode
    {
        [SerializeField] public bool usesComponent = false;
        [SerializeField] public List<Item.Entry> weapons;
        [SerializeField] public List<Item.Entry> suits;
        [SerializeField] public List<Item.Entry> helmets;
        [SerializeField] public List<Item.Entry> keyItems;

        protected override void OnStart()
        {
            state = State.Failure;

            if (usesComponent)
            {
                weapons = board.hasItem.weapons;
                suits = board.hasItem.suits;
                helmets = board.hasItem.helmets;
                keyItems = board.hasItem.keyItems;
            }

            if (Blackboard.player.getKeyItems.count == 0)
                return;

            if (weapons.TrueForAll(item => Blackboard.player.getWeapons.Count(item.name) >= item.count)
             && suits.TrueForAll(item => Blackboard.player.getSuits.Count(item.name) >= item.count)
             && helmets.TrueForAll(item => Blackboard.player.getHelmets.Count(item.name) >= item.count)
             && keyItems.TrueForAll(item => Blackboard.player.getKeyItems.Count(item.name) >= item.count))
                state = State.Running;
        }

        protected override State OnUpdate()
        {
            if (state != State.Running)
                return state;

            return children[0].Update();

        }

        protected override void OnStop() { }
    }
}
