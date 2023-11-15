using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedRagdoll : MonoBehaviour, IDestructible
{
    public GameObject prefab;
    public float power = 100f;
    public float liftY = 0.1f;

    public void OnDestruction(GameObject attacker)
    {
        var go = Instantiate(prefab, transform.position, transform.rotation);
        var ragdoll = go.GetComponent<Ragdoll>();

        var direction = transform.position - attacker.transform.position;
        direction.y = 0f;
        direction.Normalize();

        direction.y += liftY;
        ragdoll.AddForce(direction * power);
        //ragdoll.AddForce(attacker.transform.forward);
    }
}
