using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpFoce;
    [SerializeField] private Rigidbody2D _rigidbody;

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
        _rigidbody.AddForce(Vector2.up * _jumpFoce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveInput * _moveSpeed, _rigidbody.velocity.y);
    }
}
