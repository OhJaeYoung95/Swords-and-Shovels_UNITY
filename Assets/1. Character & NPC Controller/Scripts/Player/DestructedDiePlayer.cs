using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructedDiePlayer : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        CharacterStats stats = transform.GetComponent<CharacterStats>();
        if (stats.IsLive)
        {
            stats.IsLive = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            var anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("Death");
        }
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
