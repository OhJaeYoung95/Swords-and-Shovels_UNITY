using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestructedDestroyGameObject : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        CharacterStats stats = transform.GetComponent<CharacterStats>();
        if (stats.IsLive)
        {
            stats.IsLive = false;
            var agent = gameObject.GetComponent<NavMeshAgent>();
            var anim = gameObject.GetComponent<Animator>();
            agent.isStopped = true;
            anim.SetTrigger("Death");
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
