using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum SkillTypes
{
    Closed,
    Ranged,
    Area,
    Count
}

public enum Skills
{
    Stomp,
    Count,
}

public class Inventory : MonoBehaviour
{
    public Weapon[] weapons = new Weapon[3];
    public Weapon[] skills = new Weapon[3];
    public Transform weaponDummy;

    public Weapon CurrentWeapon { get; private set; }
    public GameObject weaponGo;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(weapons[0], weaponDummy);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(weapons[1], weaponDummy);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(weapons[2], weaponDummy);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UnEquipWeapon();
        }

    }

    public void EquipWeapon(Weapon newWeapon, Transform parent)
    {
        if (newWeapon == null)
            return;

        UnEquipWeapon();

        CurrentWeapon = newWeapon;
        weaponGo = Instantiate(newWeapon.prefab, parent);
    }

    public void UnEquipWeapon()
    {
        if(CurrentWeapon != null)
        {
            CurrentWeapon = null;
            Destroy(weaponGo);
        }
    }
}
