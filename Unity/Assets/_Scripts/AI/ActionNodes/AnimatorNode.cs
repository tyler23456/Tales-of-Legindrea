using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorNode : WaitNode
{
    [SerializeField] bool playAudioSource = true;
    [SerializeField] string animationParameter = "noParameter";

    protected override void OnStart()
    {
        base.OnStart();
        board.animator.SetTrigger(animationParameter);

        if (playAudioSource)
            board.audioSource.Play();
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
