using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsAlive : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.mobAnimatorController.state == "dead")
            return State.Failure;
        else
        {
            return State.Success;
        }
    }
}
