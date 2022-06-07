using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed = 1;
    [SerializeField] private bool _shoot = true;

    private Vector2 _movementInput;

    private Rigidbody _rigidbody;
    private PlayerControls _playerControls;
    private WeaponBehaviour _weapon;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();
        _weapon = GetComponentInChildren<WeaponBehaviour>();
    }

    private void OnEnable()
    {
        // If in editor or standalone build, use desktop controls
#if UNITY_STANDALONE || UNITY_EDITOR
        EnableDesktopControls();
        // If on IOS or Android, use mobile controls
#elif UNITY_IOS || UNITY_ANDROID
        EnableMobileControls();
#endif
    }

    private void OnDisable()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        DisableDesktopControls();
#elif UNITY_IOS || UNITY_ANDROID
        DisableMobileControls();
#endif
    }

    private void Update()
    {
        // Constantly fire weapon
        if (_shoot) _weapon.Fire();
    }

    private void FixedUpdate()
    {
        // Apply movement input
        Vector2 movement2D = _movementInput * _speed;
        _rigidbody.MovePosition(transform.position + (Vector3)movement2D);
    }

#region CONTROLS
    private void EnableDesktopControls()
    {
        // Enable action
        _playerControls.Desktop.Enable();

        // Assign movement input
        _playerControls.Desktop.Move.performed += OnMoveInputRecievedDesktop;
        _playerControls.Desktop.Move.canceled += OnMoveInputRecievedDesktop;
    }

    private void DisableDesktopControls()
    {
        // Disable action
        _playerControls.Desktop.Disable();

        // Unassign movement input
        _playerControls.Desktop.Move.performed -= OnMoveInputRecievedDesktop;
        _playerControls.Desktop.Move.canceled -= OnMoveInputRecievedDesktop;
    }

    private void EnableMobileControls()
    {

    }

    private void DisableMobileControls()
    {

    }
#endregion

#if UNITY_STANDALONE || UNITY_EDITOR
    public void OnMoveInputRecievedDesktop(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
#endif

#if UNITY_IOS || UNITY_ANDROID
    public void OnMoveInputRecievedMobile(InputAction.CallbackContext context)
    {

    }
#endif
}
