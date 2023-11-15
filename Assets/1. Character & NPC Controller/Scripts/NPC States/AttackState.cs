using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : NPCStateBase
{
    private float lastAttackTime;
    public AttackState(NPCController manager) : base(manager)
    {

    }

    public override void Enter()
    {
        agent.isStopped = true;
        
        agent.speed = agentSpeed;
        timer = npcCtrl.CurrentWeapon.cooldown;
        var lookPos = player.transform.position;
        lookPos.y = npcCtrl.transform.position.y;
        npcCtrl.transform.LookAt(lookPos);
        //animator.SetTrigger("Attack");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (DistanceToPlayer > npcCtrl.CurrentWeapon.range || npcCtrl.RaycastToTarget)
        {
            npcCtrl.SetState(NPCController.States.Trace);
            return;
        }
        if (lastAttackTime + npcCtrl.CurrentWeapon.cooldown < Time.time)
        {
            lastAttackTime = Time.time;

            var lookPos = player.transform.position;
            lookPos.y = npcCtrl.transform.position.y;
            npcCtrl.transform.LookAt(lookPos);

            animator.SetTrigger("Attack");
        }

    }
}
