using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GDA.Player
{
    public class LowestHealthNode : ActionNode
    {
        [SerializeField]
        float overlapSphereRadius = 100f;

        Collider[] colliders = new Collider[10];
        List<IHittable> hittables = new List<IHittable>();
        int count = 0;

        protected override void OnStart()
        {
        }

        protected override State OnUpdate()
        {
            count = Physics.OverlapSphereNonAlloc(board.user.getStatFXAnchor.getAnchor.position, overlapSphereRadius, colliders, 1<<7);

            hittables.Clear();
            for (int i = 0; i < count; i++)
                if (board.agent.transform != colliders[i].transform)
                    hittables.Add(colliders[i].GetComponent<IHittable>());

            if (hittables.Count == 0)
                return State.Failure;

            hittables = hittables.OrderBy(e => e.getStats.health).ToList();

            board.target = hittables[0];

            return State.Success;
        }

        protected override void OnStop() { }
    }
}