using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDestroyGameObject : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        Destroy(gameObject);
    }
}
