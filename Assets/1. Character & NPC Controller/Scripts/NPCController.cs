using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour, IAttackable
{
    public enum Status
    {
        Idle,
        Patrol,
        Trace,
    }

    private Status currStatus;
    public Status CurrStatus
    {
        get { return currStatus; }
        set
        {
            var preStatus = currStatus;
            currStatus = value;

            timer = 0f;
            agent.speed = speed;
            agent.isStopped = false;

            switch (currStatus)
            {
                case Status.Idle:
                    agent.isStopped = true;
                    break;
                case Status.Patrol:
                    //waypointIndex = Random.Range(0, waypoints.Length);
                    waypointIndex++;
                    waypointIndex %= waypoints.Length;
                    agent.destination = waypoints[waypointIndex].position;
                    break;
                case Status.Trace:
                    agent.speed = agentSpeed;
                    agent.destination = player.transform.position;
                    break;
            }
        }
    }

    private float timer = 0f;
    public float idleTime = 1f;
    public float traceInterval = 0.2f;

    private bool isTrace = false;

    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area
    private int waypointIndex;

    private float speed, agentSpeed; // current agent speed and NavMeshAgent component speed
    private Transform player; // reference to the player object transform
    private float distansToPlayer;

    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agentSpeed = agent.speed;
            speed = agentSpeed * 0.5f;
        }
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        CurrStatus = Status.Idle;
    }

    private void Update()
    {
        distansToPlayer = Vector3.Distance(transform.position, player.position);

        //AggroPlayer();
        switch (currStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Patrol:
                UpdatePatrol();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void UpdateIdle()
    {

        if (distansToPlayer < aggroRange)
        {
            CurrStatus = Status.Trace;
            return;
        }

        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            CurrStatus = Status.Patrol;
            return;
        }
    }

    private void UpdatePatrol()
    {
        if (distansToPlayer < aggroRange)
        {
            CurrStatus = Status.Trace;
            return;
        }

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            CurrStatus = Status.Idle;
            return;
        }
    }

    private void UpdateTrace()
    {

        if(distansToPlayer > aggroRange)
        {
            CurrStatus = Status.Idle;
            return;
        }

        timer += Time.deltaTime;
        if (timer > traceInterval)
        {
            timer = 0f;
            agent.destination = player.position;
        }

        //if (timer > traceTime)
        //{
        //    timer = 0f;
        //    agent.destination = player.transform.position;
        //}
        //if (agent.remainingDistance > aggroRange)
        //{
        //    isTrace = false;
        //    CurrStatus = Status.Idle;
        //}
    }
    private void AggroPlayer()
    {
        if (isTrace)
            return;

        if (Vector3.Distance(player.position, transform.position) < aggroRange)
        {
            Vector3 prevDest = agent.destination;
            agent.destination = player.position;
            if(agent.remainingDistance < aggroRange)
            {
                timer = 0f;
                isTrace = true;
                CurrStatus = Status.Trace;
            }
            else
                agent.destination = prevDest;
        }

        //Collider[] colliders = Physics.OverlapSphere(transform.position, aggroRange);
        //foreach (var collider in colliders)
        //{
        //    if (collider.CompareTag("Player"))
        //    {
        //        //agent.destination = collider.transform.position;
        //        CurrStatus = Status.Trace;
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        var prevColor = Gizmos.color;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = prevColor;
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {

    }
}
