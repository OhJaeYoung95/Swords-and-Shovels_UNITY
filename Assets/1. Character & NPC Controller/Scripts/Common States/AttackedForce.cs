using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackedForce : MonoBehaviour, IAttackable
{
    public float force = 2.4f;
    public float liftY = 1f;
    private Rigidbody rigid;
    private NavMeshAgent agent;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (rigid.transform.CompareTag("Enemy"))
        {
            Debug.Log(rigid.velocity.magnitude);
        }
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        CharacterStats stats = transform.GetComponent<CharacterStats>();
        if (!stats.IsLive)
            return;

        var dir = gameObject.transform.position - attacker.transform.position;
        dir.Normalize();
        dir.y += liftY;
        rigid.AddForce(dir * force, ForceMode.Impulse);
    }
}
