using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : NPCStateBase
{
    public AttackState(NPCController manager) : base(manager)
    {
    }

    public override void Enter()
    {
        timer = npcCtrl.hitInterval;
        agent.speed = agentSpeed;
        agent.isStopped = true;
        //animator.SetTrigger("Attack");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, npcCtrl.transform.position);
        timer += Time.deltaTime;

        if (timer > npcCtrl.hitInterval)
        {
            timer = 0;
            animator.SetTrigger("Attack");
        }

        if (distanceToPlayer > npcCtrl.range)
        {
            npcCtrl.SetState(NPCController.States.Trace);
            return;
        }

    }
}
