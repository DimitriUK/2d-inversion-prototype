using System;
using _Game.Core.Scripts.Objects;
using UnityEngine;

namespace _Game.Core.Scripts.Player
{
    /// <summary>
    /// TODO: Change all input to eventually use the actual InputManager from Unity Project Settings rather than
    /// this temporary solution.
    /// </summary>
    public class PlayerInputsSystem : MonoBehaviour
    {
        [SerializeField] private InputConfig _config;
        
        public event Action<float> OnMove;
        public event Action OnJumpPressed;
        
        public event Action OnAttackPressed;
        public event Action OnReloadPressed;
        public event Action<int> OnSwitchWeaponPressed;

        private const string HORIZONTAL = "Horizontal";
        private const int ZERO = 0;
        private const int ONE = 1;
        
        private void Update()
        {
           HandleInput();
        }

        private void HandleInput()
        {
            float moveDir = Input.GetAxisRaw(HORIZONTAL);
            
            OnMove?.Invoke(moveDir);

            if (Input.GetKeyDown(_config.JumpKey))
                OnJumpPressed?.Invoke();
            
            if (Input.GetMouseButtonDown(_config.MouseShootButton))
                OnAttackPressed?.Invoke();

            if (Input.GetKeyDown(_config.ReloadKey))
                OnReloadPressed?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
                OnSwitchWeaponPressed?.Invoke(ZERO);
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
                OnSwitchWeaponPressed?.Invoke(ONE);
        }
    }
}