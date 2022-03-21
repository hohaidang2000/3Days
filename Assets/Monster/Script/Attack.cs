using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Attack : ActionNode
{
    public float attackRange = 1;
   
   
    protected override void OnStart() {
       
    }

    protected override void OnStop() {
    }
    void BeginAttack()
    {
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(context.transform.position, attackRange, 3);
        
        if (targetsInViewRadius.Length > 0)
        {
            context.transform.GetComponent<Damage>().InflictDamage(context.targetTransform.targetGameObject);
        }
        context.animator.SetTrigger("attack");

    }
    protected override State OnUpdate() {

        BeginAttack();
        return State.Success;
    }
}
