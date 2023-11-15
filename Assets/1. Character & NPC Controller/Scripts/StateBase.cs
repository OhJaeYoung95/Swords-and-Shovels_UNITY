using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase
{
    abstract public void Enter();
    abstract public void Update();
    abstract public void Exit();
}

public abstract class NPCStateBase : StateBase
{
    protected NPCController npcCtrl;

    protected Animator animator; // reference to the animator component
    protected NavMeshAgent agent; // reference to the NavMeshAgent

    protected float agentSpeed, speed;
    public Transform player;
    protected float timer = 0f;

    //protected float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    protected float traceInterval = 0.2f;

    public float DistanceToPlayer
    {
        get
        {
            if(player == null)
            {
                return 0f;
            }
            return Vector3.Distance(npcCtrl.transform.position, player.transform.position);
            //return npcCtrl.agent.remainingDistance;
        }
    }


    public NPCStateBase(NPCController npcCtrl)
    {
        this.npcCtrl = npcCtrl;

        animator = npcCtrl.GetComponent<Animator>();
        agent = npcCtrl.GetComponent<NavMeshAgent>();
        player = npcCtrl.targetTr;
        agentSpeed = agent.speed;
        speed = agentSpeed * 0.5f;
    }
}