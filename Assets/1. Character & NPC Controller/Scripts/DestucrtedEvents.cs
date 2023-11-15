using System;
using UnityEngine;

public class DestucrtedEvents : MonoBehaviour, IDestructible
{
    public event Action OnEvent;

    public void OnDestruction(GameObject attacker)
    {
        if (OnEvent != null)
            OnEvent();
    }
}
