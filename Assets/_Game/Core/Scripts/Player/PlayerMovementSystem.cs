using System;
using _Game.Core.Scripts.Enums;
using _Game.Core.Scripts.Interfaces;
using UnityEngine;

namespace _Game.Core.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementSystem : MonoBehaviour, IMovable, IJumpable, IInvertable
    {
        [Header("Components")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;
        
        private Rigidbody2D _rigidbody;
        
        [Header("Configurable")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 7f;
        
        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;
        
        public MovementState CurrentState { get; private set; }
        
        public bool IsMoving => Mathf.Abs(_rigidbody.linearVelocity.x) > 0.1f;
        public bool IsGrounded => Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
        public bool IsFacingLeft { get; private set; }
        public bool IsInverted { get; private set; }
        public bool IsInvertable { get; }
        private bool IsFalling => IsInverted ? _rigidbody.linearVelocityY > 1 : _rigidbody.linearVelocityY < -1;
        
        public float DefaultGravity { get; set; } = 5;
        public float InvertedGravity { get; set; } = -5;

        public event Action<bool> OnFacingDirectionChanged;
        public event Action<bool> OnInversionChanged;

        public void Initialise()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            UpdateMovementState();
        }

        public void Move(float direction)
        {
            _rigidbody.linearVelocity = new Vector2(direction * _moveSpeed, _rigidbody.linearVelocity.y);

            if (direction == 0)
                return;
            
            bool newDirection = direction < 0;
            UpdateFacingDirection(newDirection);
        }
        
        public void Jump()
        {
            if (IsGrounded)
            {
                if (!IsInverted)
                    _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                else _rigidbody.AddForce(-Vector2.up * _jumpForce, ForceMode2D.Impulse);
                
                CurrentState = MovementState.Jumping;
            }
        }
        
        private void UpdateMovementState()
        {
            if (!IsGrounded)
                CurrentState = IsFalling ? MovementState.Falling : MovementState.Jumping;
            else
                CurrentState = IsMoving ? MovementState.Walking : MovementState.Idle;

            if (IsInverted)
            {
                CurrentState = MirrorStateForInversion(CurrentState);
            }
        }

        private MovementState MirrorStateForInversion(MovementState originalState)
        {
            return originalState switch
            {
                MovementState.Falling => MovementState.Jumping,
                MovementState.Jumping => MovementState.Falling,
                _ => originalState
            };
        }
        
        private void UpdateFacingDirection(bool newDirection)
        {
            if (IsFacingLeft == newDirection) 
                return;
            
            IsFacingLeft = newDirection;
            OnFacingDirectionChanged?.Invoke(IsFacingLeft);
        }
        
        public void Invert()
        {
            IsInverted = !IsInverted;
            _rigidbody.gravityScale = IsInverted ? InvertedGravity : DefaultGravity;
            
            OnInversionChanged?.Invoke(IsInverted);
        }
    }
}