namespace _Game.Core.Scripts.Interfaces
{
    public interface IWeapon
    {
        void Attack();
        float AttackCooldown { get; }
        bool CanAttack { get; }
        int WeaponID { get; }
    }
}