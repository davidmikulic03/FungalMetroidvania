using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "Stats/MovementStats")]
public class MovementStats : ScriptableObject
{
    [Range(0.0f, 50f)] public float maxSpeed = 4f;
    [Range(0.0f, 5f)] public float groundedAccelerationTime = 0.5f;
    [Range(0.0f, 5f)] public float airborneAccelerationTime = 1f;
    [Range(0.0f, 5f)] public float groundedDecelerationTime = 1f;
    [Range(0.0f, 5f)] public float airborneDecelerationTime = 1f;
}
