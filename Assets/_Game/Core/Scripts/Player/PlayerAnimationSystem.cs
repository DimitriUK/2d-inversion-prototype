using _Game.Core.Scripts.Enums;
using UnityEngine;

namespace _Game.Core.Scripts.Player
{
    public class PlayerAnimationSystem : MonoBehaviour
    {
        private PlayerMovementSystem _movementSystem;
        private Animator _animator;
        
        private static readonly int EquippedIdHash = Animator.StringToHash("EquippedId");
        private static readonly int MovementHash = Animator.StringToHash("Movement");
        
        public void SetEquippedId(int weaponId) => _animator.SetInteger(EquippedIdHash, weaponId);

        public void TriggerJump() => UpdateMovementState((int)MovementState.Jumping);
        
        public void Initialise(PlayerMovementSystem playerMovementSystem)
        {
            _animator = GetComponent<Animator>();
            _movementSystem = playerMovementSystem;
        }

        private void Update()
        {
            UpdateMovementAnimation();
        }
        
        private void UpdateMovementAnimation()
        {
            if (_movementSystem == null)
                return;
        
            _animator.SetFloat(MovementHash, (int)_movementSystem.CurrentState);
        }

        private void UpdateMovementState(int movementState)
        {
            _animator.SetFloat(MovementHash, movementState);
        }
    }
}