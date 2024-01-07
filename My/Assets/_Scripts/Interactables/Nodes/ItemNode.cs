using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GDA.Interactables
{
    public class ItemNode : ActionNode
    {
        [SerializeField] public bool usesComponent = false;
        [SerializeField] public bool destroyGameObject = true;
        [SerializeField] public List<Item.Entry> weapons;
        [SerializeField] public List<Item.Entry> suits;
        [SerializeField] public List<Item.Entry> helmets;
        [SerializeField] public List<Item.Entry> keyItems;

        protected override void OnStart() 
        {
            if (usesComponent)
            {
                destroyGameObject = board.item.destroyGameObject;
                weapons = board.item.weapons;
                suits = board.item.suits;
                helmets = board.item.helmets;
                keyItems = board.item.keyItems;
            }

            state = State.Running;

            //Weapons                                                                                                                                                                                                                                                                                                                                                
            List<(string, int)> weaponResults = new List<(string, int)>();
            foreach (Item.Entry item in weapons)
                weaponResults.Add((item.name, item.count));

            if (weapons.Count > 0)
                Blackboard.player.getWeapons.AddRange(weaponResults);

            //suit
            List<(string, int)> suitResults = new List<(string, int)>();
            foreach (Item.Entry item in suits)
                suitResults.Add((item.name, item.count));

            if (suits.Count > 0)
                Blackboard.player.getSuits.AddRange(suitResults);

            //suit
            List<(string, int)> helmetResults = new List<(string, int)>();
            foreach (Item.Entry item in helmets)
                helmetResults.Add((item.name, item.count));

            if (helmets.Count > 0)
                Blackboard.player.getHelmets.AddRange(helmetResults);

            //KeyItems
            List<(string, int)> keyItemResults = new List<(string, int)>();
            foreach (Item.Entry item in keyItems)
                keyItemResults.Add((item.name, item.count));

            if (keyItems.Count > 0)
                Blackboard.player.getKeyItems.AddRange(keyItemResults);
        }

        protected override State OnUpdate()
        {
            if (destroyGameObject)
            {
                board.renderer.enabled = false;
                board.interactableRunner.enabled = false;
                board.collider.enabled = false;
                Destroy(board.gameObject, 3f);

                foreach (Transform t in board.gameObject.transform)
                    if (t.TryGetComponent(out Renderer rend)) { rend.enabled = false; };
            }

            return State.Success;
        }

        protected override void OnStop() { }
    }
}