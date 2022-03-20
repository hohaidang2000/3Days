using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TheKiwiCoder;

public class PlaySound : ActionNode
{
    public AudioClip audioClip;
    protected override void OnStart() {
        context.audioSource.clip = audioClip;
        if(context.audioSource.isPlaying == false)
            context.audioSource.Play();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
