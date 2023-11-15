using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private CharacterStats player;
    public Weapon CurrentWeapon { get; set; }
    //public float speed = 4f;

    public event Action<GameObject, GameObject> OnCollided;

    private Rigidbody rb;
    private Vector3 velocity;
    private float distance;
    private Vector3 startPos;
    private GameObject caster;

    public void Fire(GameObject caster, Vector3 v, float d)
    {
        velocity = v;
        distance = d;
        startPos = transform.position;
        this.caster = caster;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    private void FixedUpdate()
    {
        var pos = rb.position;
        if (Vector3.Distance(startPos, pos) > distance)
        {
            Destroy(gameObject);
            return;
        }
        pos += velocity * Time.deltaTime;
        rb.MovePosition(pos);
    }

    //void Update()
    //{
    //    transform.position += speed * transform.forward * Time.deltaTime;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (OnCollided != null)
            OnCollided(caster, other.gameObject);
        Debug.Log(other.gameObject.name);
        Destroy(gameObject);

        //var aStats = gameObject.GetComponent<CharacterStats>();
        //var dStats = player;
        //var attack = CurrentWeapon.CreateAttack(aStats, dStats);

        //if (other.CompareTag("Player"))
        //{
        //    var attackables = other.GetComponents<IAttackable>();
        //    if (attackables != null)
        //    {
        //        foreach( var attackable in attackables )
        //        {
        //            attackable.OnAttack(gameObject, attack);
        //        }
        //    }
        //    Debug.Log("Throw Hit");
        //    Destroy(gameObject);
        //}

    }
}
