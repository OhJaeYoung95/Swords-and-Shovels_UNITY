using UnityEngine;

public class TraceState : NPCStateBase
{

    public TraceState(NPCController2 manager) : base(manager)
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
        float distanceToPlayer = Vector3.Distance(player.position, npcCtrl.transform.position);

        if (distanceToPlayer > aggroRange)
        {
            npcCtrl.SetState(NPCController2.States.Idle);
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
