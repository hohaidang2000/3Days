using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckIsNotInrange : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        if (context.fieldOfView.seePlayer)
        {
            return State.Failure;
        }
        return State.Success;

    }
}
