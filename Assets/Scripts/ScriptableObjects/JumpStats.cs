using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpStats", menuName = "Stats/JumpStats")]
public class JumpStats : ScriptableObject
{
    [Range(0f, 10f)] public float jumpHeight = 3f;
    [Range(0f, 5f)] public float fallSpeedMultiplier = 2;
    [Range(0f, 1f)] public float coyoteTime = 0.2f;
    [Range(0f, 1f)] public float bufferTime = 0.5f;

}
