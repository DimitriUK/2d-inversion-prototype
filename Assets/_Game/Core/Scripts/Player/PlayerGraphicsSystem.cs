using UnityEngine;

namespace _Game.Core.Scripts.Player
{
    public class PlayerGraphicsSystem : MonoBehaviour
    {
        private PlayerMovementSystem _movementSystem;
        
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;

        private const float ONE = 1f;

        public void Initialise(PlayerMovementSystem playerMovementSystem)
        {
            _movementSystem = playerMovementSystem;
        }
        
        private void OnEnable() => _movementSystem.OnInversionChanged += UpdateVisuals;
        private void OnDisable() => _movementSystem.OnInversionChanged -= UpdateVisuals;
        
        public void UpdateFacingDirection(bool isFacingLeft)
        {
            _playerSpriteRenderer.transform.localScale = isFacingLeft ? new Vector3(ONE, _playerSpriteRenderer.transform.localScale.y) : new Vector3(-ONE, _playerSpriteRenderer.transform.localScale.y, ONE);
        }

        private void UpdateVisuals(bool isInverted)
        {
            _playerSpriteRenderer.transform.localScale = isInverted
                ? new Vector3(_playerSpriteRenderer.transform.localScale.x, -ONE, _playerSpriteRenderer.transform.localScale.z) 
                : new Vector3(_playerSpriteRenderer.transform.localScale.x, ONE, _playerSpriteRenderer.transform.localScale.z);
        }
    }
}