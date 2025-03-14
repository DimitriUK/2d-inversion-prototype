namespace _Game.Core.Scripts.Interfaces
{
    public interface IMovable
    {
        float MoveSpeed { get; }
        void Move(float direction);
        bool IsMoving { get; }
    }
}