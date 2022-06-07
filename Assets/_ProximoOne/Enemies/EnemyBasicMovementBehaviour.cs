using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBasicMovementBehaviour : EnemyMovementBase
{
    [SerializeField] private Vector3 _movement = new Vector3();
    [SerializeField] private bool _shoot = true;

    private Rigidbody _rigidbody;
    private WeaponBehaviour _weapon;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weapon = GetComponentInChildren<WeaponBehaviour>();
    }

    private void Update()
    {
        if (_shoot) _weapon.Fire();
    }

    private void FixedUpdate()
    {
        if (AllowMovement)
            _rigidbody.MovePosition(transform.position + _movement * Time.deltaTime);
    }
}
