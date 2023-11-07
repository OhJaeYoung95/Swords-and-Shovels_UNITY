using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedForce : MonoBehaviour, IAttackable
{
    public float force = 2.4f;
    public float liftY = 1f;
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var dir = gameObject.transform.position - attacker.transform.position;
        dir.Normalize();
        dir.y += liftY;
        rigid.AddForce(dir * force, ForceMode.Impulse);
    }
}
