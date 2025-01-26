using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private const float SURFACE_RADIUS_CHECK = 0.1f;
    private const int GROUND_LAYER = 1 << 6;

    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpFoce;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundCheck;
    
    private bool _isGrounded;
    private float _moveInput;
    private PlayerInputActions _inputActions;
    
    private void Awake()
    {
        _inputActions = new();
        _inputActions.Player.Move.performed += Move;
        _inputActions.Player.Move.canceled += Stop;
        _inputActions.Player.Jump.performed += Jump;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Stop(InputAction.CallbackContext context)
    {
        _moveInput = 0;
    }

    private void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>().x;
    }
    
    private void Jump(InputAction.CallbackContext obj)
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpFoce, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, SURFACE_RADIUS_CHECK, GROUND_LAYER);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveInput * _moveSpeed, _rigidbody.velocity.y);
    }
}
