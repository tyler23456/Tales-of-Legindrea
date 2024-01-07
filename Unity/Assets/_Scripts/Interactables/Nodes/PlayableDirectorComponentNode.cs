using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;

namespace GDA.Interactables
{
    public class PlayableDirectorComponentNode : ActionNode
    {
        protected override void OnStart()
        {
            foreach (GameObject obj in board.playableBinder.disableReferences)
            {                
                if (obj.TryGetComponent(out NavMeshAgent nav)) { nav.enabled = false; }
                if (obj.TryGetComponent(out IHittable hit)) { hit.enabled = false; }
            }
            
            state = State.Running;
            board.playableDirector.Play();
            board.playableDirector.stopped += (i) => state = State.Success;
        }

        protected override State OnUpdate()
        {
            return state;
        }

        protected override void OnStop() 
        {
            foreach (GameObject obj in board.playableBinder.enableReferences)
            {
                if (obj.TryGetComponent(out NavMeshAgent nav)) { nav.enabled = true; }
                if (obj.TryGetComponent(out IHittable hit)) { hit.enabled = true; }
            }
        }
    }
}