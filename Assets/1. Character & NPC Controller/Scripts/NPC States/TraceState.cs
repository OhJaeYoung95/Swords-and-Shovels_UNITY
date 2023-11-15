using UnityEngine;

public class TraceState : NPCStateBase
{

    public TraceState(NPCController manager) : base(manager)
    {
    }

    public override void Enter()
    {
        timer = 0f;
        agent.isStopped = false;
        agent.speed = agentSpeed;
        agent.destination = player.transform.position;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (DistanceToPlayer <= npcCtrl.CurrentWeapon.range && !npcCtrl.RaycastToTarget)
        {
            npcCtrl.SetState(NPCController.States.Attack);
            return;
        }

        if (DistanceToPlayer > npcCtrl.aggroRange)
        {
            npcCtrl.SetState(NPCController.States.Idle);
            return;
        }


        timer += Time.deltaTime;
        if (timer > traceInterval)
        {
            timer = 0f;
            agent.destination = player.position;
        }

    }
}
