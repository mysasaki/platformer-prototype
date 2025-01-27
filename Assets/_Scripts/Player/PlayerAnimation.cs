using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string MOVE_KEY = "Move";
    private const string JUMP_KEY = "IsJumping";
        
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _movement;
    
    private void OnEnable()
    {
        _movement.OnJump += Jump;
    }

    private void OnDisable()
    {
        _movement.OnJump -= Jump;
    }

    private void Jump()
    {
        Debug.Log("Jump");
        _animator.SetTrigger(JUMP_KEY);
    }

    private void FixedUpdate()
    {
        int speed = (int) Mathf.Abs(_movement.MoveSpeed);
        _animator.SetInteger(MOVE_KEY, speed);
    }
}
