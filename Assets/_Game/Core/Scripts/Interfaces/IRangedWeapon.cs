namespace _Game.Core.Scripts.Interfaces
{
    public interface IRangedWeapon : IWeapon
    {
        void Reload();
        int CurrentAmmo { get; }
        int MaxAmmo { get; }
    }
}