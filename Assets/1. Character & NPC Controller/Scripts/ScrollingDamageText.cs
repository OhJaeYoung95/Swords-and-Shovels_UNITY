using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollingDamageText : MonoBehaviour, IAttackable
{
    public Color color = Color.white;
    public Scrolling damageText;
    public float offsetY = 2f;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var position = transform.position;
        position.y += offsetY;

        var text = Instantiate(damageText, position, Quaternion.identity);
        text.Set(attack.Damage.ToString(), color);
    }

    //public void OnAttack(GameObject attacker, Attack attack)
    //{
    //    if(attack.IsCritical)
    //    {
    //        damageText.color = Color.yellow;
    //    }
    //    else
    //    {
    //        damageText.color = Color.white;
    //    }


    //    var dir = Camera.main.transform.position - transform.position;
    //    dir.Normalize();
    //    dir.y += offsetY;
    //    damageText.transform.LookAt(dir);
    //    damageText.text = $"{attack.Damage}";
    //    Instantiate(damageText, gameObject.transform);
    //}
}
