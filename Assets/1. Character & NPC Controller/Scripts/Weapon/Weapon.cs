using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
    public SkillTypes skillType;
    public GameObject prefab;
    public ParticleSystem particle;
    public ParticleSystem trail;

    public void ExecuteClosedAttack(GameObject attacker, GameObject defender)
    {
        // 애니메이션 => 타격
        if (defender == null)
            return;

        // 거리
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
            return;

        if (trail != null)
        {
            var trailPos = attacker.transform.position + Vector3.up * 2;
            ParticleSystem newTrail = Instantiate(trail, trailPos, Quaternion.identity);
        }

        if (particle != null)
        {
            ParticleSystem newParticle = Instantiate(particle, defender.transform.position, Quaternion.identity);
        }


        // 방향
        var dir = defender.transform.position - attacker.transform.position;
        dir.Normalize();

        var dot = Vector3.Dot(dir, attacker.transform.forward);
        if (dot < 0.5f)
            return;

        var aStats = attacker.GetComponent<CharacterStats>();
        var dStats = defender.GetComponent<CharacterStats>();
        var attack = CreateAttack(aStats, dStats);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }

    public void ExecuteAreaAttack(GameObject attacker)
    {
        if (!particle)
            return;
        Vector3 particlePos = attacker.transform.position + Vector3.up * 1;
        ParticleSystem newParticle = Instantiate(particle, particlePos, Quaternion.identity);

        var colliders = Physics.OverlapSphere(attacker.transform.position, this.range);
        if (colliders.Length == 0)
            return;
        foreach (var collider in colliders)
        {
            var attackables = collider.GetComponents<IAttackable>();
            foreach (var attackable in attackables)
            {
                if (attackable != null && collider.CompareTag("Enemy"))
                {
                    var aStats = attacker.GetComponent<CharacterStats>();
                    var dStats = collider.GetComponent<CharacterStats>();
                    var attack = CreateAttack(aStats, dStats);

                    attackable.OnAttack(attacker, attack);
                }
            }
        }

    }
}
