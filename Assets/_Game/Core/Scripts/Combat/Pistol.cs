using _Game.Core.Scripts.Interfaces;
using _Game.Core.Scripts.Player;
using UnityEngine;

namespace _Game.Core.Scripts.Combat
{
    public class Pistol : RangedWeaponBase, IMovableWeapon
    {
        private PlayerMovementSystem _movementSystem;

        public void InitialiseMovement(PlayerMovementSystem movementSystem)
        {
            _movementSystem = movementSystem;
        }

        protected override void ConfigureBullet(Bullet bullet)
        {
            GetFireParameters(out Vector2 direction, out Vector3 positionOffset);
            bullet.transform.position = _firePoint.position + positionOffset;
            bullet.SetDirection(direction);

            if (_movementSystem.IsInverted)
                bullet.Invert();
        }

        private void GetFireParameters(out Vector2 direction, out Vector3 positionOffset)
        {
            bool isFacingLeft = _movementSystem.IsFacingLeft;

            direction = isFacingLeft ? Vector2.left : Vector2.right;
            positionOffset = new Vector3(
                isFacingLeft ? -_config.XFiringOffset : _config.XFiringOffset,
                0f,
                0f
            );
        }
    }
}