using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class OtherAnimatorNode : ActionNode
    {
        [SerializeField] bool playOtherAudioSource = true;
        [SerializeField] string trigger;

        protected override void OnStart()
        {
            state = State.Running;
            board.otherAnimator.getAnimator.SetTrigger(trigger);

            if (playOtherAudioSource)
                board.otherAnimator.getAnimator.transform.GetComponent<AudioSource>().Play();
        }

        protected override State OnUpdate()
        {
            state = State.Success;
            return state;
        }

        protected override void OnStop() { }
    }
}