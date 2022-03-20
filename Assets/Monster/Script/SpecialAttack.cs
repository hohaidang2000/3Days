using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SpecialAttack : ActionNode
{
    public float pullSpeed = 5;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.animator.SetTrigger("attack");
        Vector3 dirToTarget = (context.transform.position - context.targetTransform.targetGameObject.transform.position).normalized;
        context.targetTransform.tartgetController.Move(dirToTarget*pullSpeed*Time.deltaTime);
        return State.Success;
    }
}
