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
        if (_shootInput)
            _weapon.Fire();
    }

    private void FixedUpdate()
    {
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

    public void TakeDamage(GameObject source, int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

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
