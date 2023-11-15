using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private CharacterStats stats;
    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        CharacterStats stats = transform.GetComponent<CharacterStats>();
        if (!stats.IsLive)
            return;

        HitNPCEffect();
        stats.Hp -= attack.Damage;
        stats.Hp = (int)Mathf.Clamp(stats.Hp, 0f, stats.maxHp);

        if (stats.Hp <= 0f)
        {
            var destructbales = GetComponents<IDestructible>();
            foreach (var destructable in destructbales)
            {
                destructable.OnDestruction(attacker);
            }
        }
        //Debug.Log(stats.Hp);
    }

    public void HitNPCEffect()
    {
        if(gameObject.CompareTag("Enemy"))
        {
            NPCController npc = gameObject.GetComponent<NPCController>();
            //npc.attackEffect.SetActive(true);
            //npc.attackParticle.Play();
        }
    }
}
