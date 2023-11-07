using UnityEngine;

public class PatrolState : NPCStateBase
{

    public PatrolState(NPCController2 manager) : base(manager)
    {
    }

    public override void Enter()
    {
        timer = 0f;
        agent.isStopped = false;
        npcCtrl.waypointIndex++;
        npcCtrl.waypointIndex %= npcCtrl.waypoints.Length;
        agent.destination = npcCtrl.waypoints[npcCtrl.waypointIndex].position;

    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, npcCtrl.transform.position);

        if (distanceToPlayer < aggroRange)
        {
            npcCtrl.SetState(NPCController2.States.Trace);
            return;
        }

        if (!agent.pathPending && (agent.remainingDistance < agent.stoppingDistance))
        {
            npcCtrl.SetState(NPCController2.States.Idle);
            return;
        }

    }
}
