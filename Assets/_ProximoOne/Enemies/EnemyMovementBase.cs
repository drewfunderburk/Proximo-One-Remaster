using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementBase : MonoBehaviour
{
    [SerializeField] private bool _allowMovement = true;

    public bool AllowMovement { get => _allowMovement; set => _allowMovement = value; }
}
