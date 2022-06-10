using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Stage")]
public class StageSO : ScriptableObject
{
    // List of waves to spawn
    public Wave[] Waves;

    [Space]
    [Tooltip("How long to wait between wave spawns")]
    public float WaveDelay = 3;
}
