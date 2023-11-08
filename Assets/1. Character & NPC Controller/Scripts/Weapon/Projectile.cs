using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private CharacterStats player;
    public Weapon CurrentWeapon { get; set; }
    public float speed = 4f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var aStats = gameObject.GetComponent<CharacterStats>();
        var dStats = player;
        var attack = CurrentWeapon.CreateAttack(aStats, dStats);

        if (other.CompareTag("Player"))
        {
            var attackables = other.GetComponents<IAttackable>();
            if (attackables != null)
            {
                foreach( var attackable in attackables )
                {
                    attackable.OnAttack(gameObject, attack);
                }
            }
            Debug.Log("Throw Hit");
            Destroy(gameObject);
        }

    }
}
