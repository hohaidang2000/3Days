using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TheKiwiCoder;

public class PlaySound : ActionNode
{
    protected override void OnStart() {
        context.audioSource.Play();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
