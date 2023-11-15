using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody rigid;
    public float duration = 3f;

    private void Awake()
    {
        Destroy(gameObject, duration);
    }

    public void AddForce(Vector3 force)
    {
        rigid.AddForce(force, ForceMode.Impulse);
    }
}
