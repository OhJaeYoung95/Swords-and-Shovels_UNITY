using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public Transform exit;

    public Vector3 GetExit()
    {
        return exit.position;
    }
}
