using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsAlive : ActionNode
{
    public AudioClip clip;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.mobAnimatorController.state == "dead")
        {
            context.audioSource.clip = clip;
            context.audioSource.Play();
            return State.Failure;
        }
        else
        {
            return State.Success;
        }
    }
}
