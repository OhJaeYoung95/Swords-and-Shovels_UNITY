using UnityEngine;

public class PatrolState : NPCStateBase
{

    public PatrolState(NPCController manager) : base(manager)
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

        if (distanceToPlayer < npcCtrl.aggroRange)
        {
            npcCtrl.SetState(NPCController.States.Trace);
            return;
        }

        if (!agent.pathPending && (agent.remainingDistance < agent.stoppingDistance))
        {
            npcCtrl.SetState(NPCController.States.Idle);
            return;
        }

    }
}
