namespace _Game.Core.Scripts.Interfaces
{
    public interface IMeleeWeapon : IWeapon
    {
        int BluntDamage { get; }
        int SharpDamage { get; }
    }
}