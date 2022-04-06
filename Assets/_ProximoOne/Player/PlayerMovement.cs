using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 1;

    private Vector2 _movementInput;
    private bool _shootInput;

    private Rigidbody _rigidbody;
    private PlayerControls _playerControls;

    #region SETUP METHODS
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Enable action
        _playerControls.Default.Move.Enable();

        // Assign movement input
        _playerControls.Default.Move.performed += OnMoveInputRecieved;
        _playerControls.Default.Move.canceled += OnMoveInputRecieved;
    }

    private void OnDisable()
    {
        // Disable action
        _playerControls.Default.Move.Disable();

        // Unassign movement input
        _playerControls.Default.Move.performed -= OnMoveInputRecieved;
        _playerControls.Default.Move.canceled -= OnMoveInputRecieved;
    }
    #endregion

    private void FixedUpdate()
    {
        Vector2 movement2D = _movementInput * _speed;
        _rigidbody.MovePosition(transform.position + (Vector3)movement2D);
    }

    #region INPUT METHODS
    public void OnMoveInputRecieved(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    #endregion
}
