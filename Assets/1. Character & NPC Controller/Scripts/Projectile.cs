using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //var aStats = attacker.GetComponent<CharacterStats>();
        //var dStats = defender.GetComponent<CharacterStats>();
        //var attack = CreateAttack(aStats, dStats);
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
