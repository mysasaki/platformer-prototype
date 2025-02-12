using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float SURFACE_RADIUS_CHECK = 0.1f;
    private const int GROUND_LAYER = 1 << 6;

    public event Action OnJump;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private ScriptableEvent _jumpEvent;
    
    private bool _isGrounded;
    private float _moveInput;
    private PlayerInputActions _inputActions;

    public float MoveSpeed => _rigidbody.velocity.x;
    private bool IsPlayMode => LevelManager.Instance.State == LevelManager.GameState.Play;
    
    private void OnEnable()
    {
        _inputActions = LevelManager.Instance.InputActions;
        _inputActions.Player.Move.performed += Move;
        _inputActions.Player.Move.canceled += Stop;
        _inputActions.Player.Jump.performed += Jump;

        LevelManager.OnLevelFinish.Subscribe(HandleLevelFinish);
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= Move;
        _inputActions.Player.Move.canceled -= Stop;
        _inputActions.Player.Jump.performed -= Jump;

        LevelManager.OnLevelFinish.Unsubscribe(HandleLevelFinish);
    }

    private void Stop(InputAction.CallbackContext context)
    {
        _moveInput = 0;
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (!IsPlayMode)
        {
            return;
        }
        
        _moveInput = context.ReadValue<Vector2>().x;

        if (_moveInput > 0)
        {
            transform.localScale = new(1, 1, 1);
        }
        else
        {
            transform.localScale = new(-1, 1, 1);
        }
    }
    
    private void Jump(InputAction.CallbackContext obj)
    {
        if (!IsPlayMode)
        {
            return;
        }
        
        if (_isGrounded)
        {
            OnJump?.Invoke();
            
            _jumpEvent.Raise();
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if (!IsPlayMode)
        {
            return;
        }
        
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, SURFACE_RADIUS_CHECK, GROUND_LAYER);
    }

    private void FixedUpdate()
    {
        if (!IsPlayMode)
        {
            return;
        }
        
        _rigidbody.velocity = new Vector2(_moveInput * _moveSpeed, _rigidbody.velocity.y);
    }

    private void HandleLevelFinish(bool gameWon)
    {
        if (gameWon)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
        }
    }
}
