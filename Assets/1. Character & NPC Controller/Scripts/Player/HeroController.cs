using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    private Inventory inventory;
    Animator animator; // reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent

    private GameObject attackTarget;

    private Coroutine coMoveAndAttack;
    private Weapon CurrentSkill { get; set; }

    void Awake()
    {
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void SetDestination(Vector3 destination)
    {
        if (coMoveAndAttack != null)
        {
            StopCoroutine(coMoveAndAttack);
            coMoveAndAttack = null;
        }

        attackTarget = null;
        agent.isStopped = false;
        agent.destination = destination;
    }

    public void AttackTarget(GameObject target)
    {
        if (coMoveAndAttack != null)
        {
            StopCoroutine(coMoveAndAttack);
            coMoveAndAttack = null;
        }

        attackTarget = target;
        coMoveAndAttack = StartCoroutine(CoMoveAndAttack());
    }

    private IEnumerator CoMoveAndAttack()
    {
        var range = 0f;
        if (inventory != null && inventory.CurrentWeapon != null)
            range = inventory.CurrentWeapon.range;

        agent.isStopped = false;
        var distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        while (distance > range)
        {
            agent.destination = attackTarget.transform.position;
            yield return new WaitForSeconds(0.1f);
            distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        }

        agent.isStopped = true;

        if (inventory != null & inventory.CurrentWeapon != null)
        {
            var lookPos = attackTarget.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
            animator.SetTrigger("Attack");
        }
    }

    public void Hit()
    {
        if (inventory == null & inventory.CurrentWeapon == null)
            return;

        inventory.CurrentWeapon.ExecuteClosedAttack(gameObject, attackTarget);
    }

    public void UseSkill(Skills skill)
    {
        if ((int)skill < 0 || (int)skill >= (int)Skills.Count)
            return;

        animator.SetTrigger(skill.ToString());
        CurrentSkill = inventory.skills[(int)skill];
    }

    public void AttackBySkill()
    {
        if (CurrentSkill == null)
            return;

        switch(CurrentSkill.skillType)
        {
            case SkillTypes.Closed:
                CurrentSkill.ExecuteClosedAttack(gameObject, attackTarget);
                break;
            case SkillTypes.Ranged:
                break;
            case SkillTypes.Area:
                CurrentSkill.ExecuteAreaAttack(gameObject);
                break;

        }
        CurrentSkill = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5);
    }

}