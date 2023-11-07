using System.Threading;
using UnityEngine;

public class IdleState : NPCStateBase
{

    public IdleState(NPCController manager) : base(manager)
    {
    }

    public override void Enter()
    {
        timer = 0f;
        agent.speed = speed;
        agent.isStopped = true;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, npcCtrl.transform.position);
        if (distanceToPlayer < npcCtrl.aggroRange)
        {
            npcCtrl.SetState(NPCController.States.Trace);
            return;
        }

        timer += Time.deltaTime;
        if (timer > npcCtrl.idleTime)
        {
            npcCtrl.SetState(NPCController.States.Patrol);
            return;
        }
    }
}
