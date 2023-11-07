using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController2 : MonoBehaviour, IAttackable
{
    public enum States
    {
        Idle,
        Patrol,
        Trace,
    }

    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    public float idleTime = 1f;
    public float traceInterval = 0.2f;

    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area
    public int waypointIndex;

    public Transform player; // reference to the player object transform

    private Animator animator; // reference to the animator component
    private NavMeshAgent agent; // reference to the NavMeshAgent

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        states.Add(new IdleState(this));
        states.Add(new PatrolState(this));
        states.Add(new TraceState(this));

        SetState(States.Idle);
    }

    // Update is called once per frame
    private void Update()
    {
        stateManager.Update();
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void SetState(States newState)
    {
        stateManager.ChangeState(states[(int)newState]);
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {

    }
}
