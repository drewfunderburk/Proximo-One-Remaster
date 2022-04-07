using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IDamageable
{

    [SerializeField] private float _speed = 1;
    [SerializeField] private int _maxHealth = 5;

    private int _health;

    private Vector2 _movementInput;
    private bool _shootInput;

    private Rigidbody _rigidbody;
    private PlayerControls _playerControls;
    private WeaponBehaviour _weapon;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();
        _weapon = GetComponentInChildren<WeaponBehaviour>();
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    private void OnEnable()
    {
        // Enable action
        _playerControls.Default.Enable();

        // Assign movement input
        _playerControls.Default.Move.performed += OnMoveInputRecieved;
        _playerControls.Default.Move.canceled += OnMoveInputRecieved;

        // Assign shoot input
        _playerControls.Default.Shoot.performed += OnShootInputRecieved;
        _playerControls.Default.Shoot.canceled += OnShootInputRecieved;
    }

    private void OnDisable()
    {
        // Disable action
        _playerControls.Default.Disable();

        // Unassign movement input
        _playerControls.Default.Move.performed -= OnMoveInputRecieved;
        _playerControls.Default.Move.canceled -= OnMoveInputRecieved;

        // Unassign shoot input
        _playerControls.Default.Shoot.performed -= OnShootInputRecieved;
        _playerControls.Default.Shoot.canceled -= OnShootInputRecieved;
    }

    private void Update()
    {
        if (_shootInput)
            _weapon.Fire();
    }

    private void FixedUpdate()
    {
        Vector2 movement2D = _movementInput * _speed;
        _rigidbody.MovePosition(transform.position + (Vector3)movement2D);
    }

    public void TakeDamage(GameObject source, int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    #region INPUT METHODS
    public void OnMoveInputRecieved(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnShootInputRecieved(InputAction.CallbackContext context)
    {
        _shootInput = context.ReadValue<float>() == 1 ? true : false;
    }
    #endregion
}
