using UnityEngine;

namespace _Game.Core.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovementSystem _movementSystem;
        [SerializeField] private PlayerAnimationSystem _animationSystem;
        [SerializeField] private PlayerInputsSystem _inputsSystem;
        [SerializeField] private PlayerGraphicsSystem _graphicsSystem;
        [SerializeField] private PlayerWeaponsSystem _weaponsSystem;

        private void Awake()
        {
            InitialiseSystems();
        }

        private void OnEnable()
        {
            InitialiseEvents();
        }

        private void InitialiseSystems()
        {
            _movementSystem.Initialise();
            _animationSystem.Initialise(_movementSystem);
            _weaponsSystem.Initialise(_animationSystem, _inputsSystem, _movementSystem);
            _graphicsSystem.Initialise(_movementSystem);
        }

        private void InitialiseEvents()
        {
            _movementSystem.OnFacingDirectionChanged += _graphicsSystem.UpdateFacingDirection;
            _inputsSystem.OnMove += HandleMove;
            _inputsSystem.OnJumpPressed += HandleJump;
            _inputsSystem.OnAttackPressed += HandleAttack;
            _inputsSystem.OnReloadPressed += HandleReload;
        }

        private void HandleAttack() => _weaponsSystem.Attack();
        private void HandleReload() => _weaponsSystem.Reload();

        private void HandleMove(float direction)
        {
            _movementSystem.Move(direction);
        }

        private void HandleJump()
        {
            _movementSystem.Jump();
            _animationSystem.TriggerJump();
        }

        private void OnDisable()
        {
            _inputsSystem.OnMove -= HandleMove;
            _inputsSystem.OnJumpPressed -= HandleJump;
            _movementSystem.OnFacingDirectionChanged -= _graphicsSystem.UpdateFacingDirection;
            _inputsSystem.OnAttackPressed -= HandleAttack;
            _inputsSystem.OnReloadPressed -= HandleReload;
        }
    }
}