using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Attack : ActionNode
{
    public float attackRange = 6;
   
   
    protected override void OnStart() {
       
    }

    protected override void OnStop() {
    }
    void BeginAttack()
    {
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(context.transform.position, attackRange);
        
  
        if (targetsInViewRadius.Length > 0)
        {
            foreach (Collider hit in targetsInViewRadius)
                if (hit.gameObject.tag == "Player")
                {
                    Debug.Log("damage");
                    context.gameObject.GetComponent<Damage>().InflictDamage(hit.gameObject);
                    context.animator.SetTrigger("attack");
                 }
           
        }
        

    }
    protected override State OnUpdate() {

        BeginAttack();
        return State.Success;
    }
}
