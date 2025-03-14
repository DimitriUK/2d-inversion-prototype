using _Game.Core.Scripts.Interfaces;

namespace _Game.Core.Scripts.Combat
{
    /// <summary>
    /// TODO: Need to create a scriptable object for this.
    /// </summary>
    public class Unarmed : IMeleeWeapon
    {
        public float AttackCooldown { get; }
        public bool CanAttack { get; }
        public int WeaponID { get; } = 0;
        public int BluntDamage { get; }
        public int SharpDamage { get; }

        public void Attack()
        {
            //TODO: If I can get some fighting animations, I'll probably add some punching and make a combo system.
        }
    }
}