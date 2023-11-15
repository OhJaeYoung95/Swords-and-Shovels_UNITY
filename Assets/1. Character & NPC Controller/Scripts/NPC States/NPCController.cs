using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour, IAttackByHand, IAttackByProjectile
{
    public enum States
    {
        Idle,
        Patrol,
        Trace,
        Attack,
        GameOver,
    }

    public enum Types
    {
        None = -1,
        Normal,
        Rock_Thrower,
        Count
    }

    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    public Types type = Types.None;

    public float idleTime = 1f;
    public float traceInterval = 0.2f;

    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area
    public int waypointIndex;

    public Transform targetTr; // reference to the player object transform

    private Animator animator; // reference to the animator component
    public NavMeshAgent agent; // reference to the NavMeshAgent

    public float hitInterval = 1f;

    public GameObject projectile;
    public GameObject attackEffect;
    public ParticleSystem attackParticle;

    public Transform firePos;

    public float range = 2f;

    public Weapon[] weapons = new Weapon[2];
    public Weapon CurrentWeapon { get; private set; }

    public AttackDefinition attackDef;
    public bool RaycastToTarget
    {
        get
        {
            var origin = transform.position;
            origin.y += 1f;

            var target = targetTr.position;
            target.y += 1f;

            var direction = target - origin;
            direction.Normalize();

            //var layer = 0xFFFF ^ LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
            int mask = LayerMask.GetMask(
                LayerMask.LayerToName(gameObject.layer),
                LayerMask.LayerToName(targetTr.gameObject.layer));
            var layer = Physics.AllLayers ^ mask;

            return Physics.Raycast(origin, direction, attackDef.range, layer);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        targetTr = GameObject.FindWithTag("Player").transform;

        if(targetTr != null)
        {
            var playerDieEvent = targetTr.GetComponent<DestucrtedEvents>();
            if (playerDieEvent != null)
            {
                playerDieEvent.OnEvent += OnPlyaerDie;
            }
        }
    }

    private void OnDestroy()
    {
        if (targetTr != null)
        {
            var playerDieEvent = targetTr.GetComponent<DestucrtedEvents>();
            if (playerDieEvent != null)
            {
                playerDieEvent.OnEvent -= OnPlyaerDie;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        states.Add(new IdleState(this));
        states.Add(new PatrolState(this));
        states.Add(new TraceState(this));
        states.Add(new AttackState(this));
        states.Add(new GameOverState(this));

        type = (Types)Random.Range(0, (int)Types.Count);
        CurrentWeapon = weapons[(int)type];
        SetState(States.Idle);
    }
    public void OnPlyaerDie()
    {
        SetState(States.GameOver);
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



    public void OnAttackByHand(GameObject attacker, GameObject defender)
    {
        // 애니메이션 => 타격
        if (defender == null)
            return;

        // 거리
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
            return;

        var dir = defender.transform.position - attacker.transform.position;
        dir.Normalize();

        var dot = Vector3.Dot(dir, attacker.transform.forward);
        if (dot < 0.5f)
            return;

        var aStats = attacker.GetComponent<CharacterStats>();
        var dStats = defender.GetComponent<CharacterStats>();
        var attack = CurrentWeapon.CreateAttack(aStats, dStats);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }

    public void OnAttackByProjectile(GameObject attacker, GameObject defender)
    {
        Projectile fireProjectile = Instantiate(attacker, firePos.position, firePos.rotation).GetComponent<Projectile>();
        fireProjectile.CurrentWeapon = CurrentWeapon;
        Destroy(fireProjectile, 5f);
    }

    public void Hit()
    {
        if (targetTr == null)
            return;

        attackDef.ExecuteAttack(gameObject, targetTr.gameObject);
        //switch (type)
        //{
        //    case Types.Normal:
        //        OnAttackByHand(gameObject, targetTr.gameObject);
        //        break;
        //    case Types.Rock_Thrower:
        //        OnAttackByProjectile(CurrentWeapon.prefab, targetTr.gameObject);
        //        break;
        //}

        //if (CurrentWeapon is Weapon)
        //{

        //}
    }
    //public void OnAttack(GameObject attacker, Attack attack)
    //{

    //}
}
