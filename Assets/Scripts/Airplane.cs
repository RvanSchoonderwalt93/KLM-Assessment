using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[CreateAssetMenu]
public class Airplane : ScriptableObject
{
    public int planeNumber;
    public int hangarNumber;

    public float minMaxX;
    public float minMaxZ;

    public float speed;
    public float acceleration;
    public float angularSpeed;
    public bool autoBreaking;

    public Vector3 destination;
}
