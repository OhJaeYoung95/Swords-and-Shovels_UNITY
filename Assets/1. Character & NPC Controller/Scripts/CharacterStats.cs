using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHp;
    public float damage;
    public int armor;
    public bool IsLive { get; set; } = true;

    public int Hp { get; set; }

    private void Awake()
    {
        Hp = maxHp;
    }
}
