namespace _Game.Core.Scripts.Interfaces
{
    public interface IInvertable
    {
        float DefaultGravity { get; set; }
        float InvertedGravity { get; set; }
        bool IsInverted { get; }
        void Invert();
    }
}