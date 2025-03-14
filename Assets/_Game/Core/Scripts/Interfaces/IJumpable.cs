namespace _Game.Core.Scripts.Interfaces
{
    public interface IJumpable
    {
        float JumpForce { get; }
        bool IsGrounded { get; }
        void Jump();
    }
}