using _Game.Core.Scripts.Interfaces;
using UnityEngine;

namespace _Game.Core.Scripts.Gameplay
{
    public class InteractableBox : MonoBehaviour, IInvertable
    {
        private Rigidbody2D _rigidbody;
        public float DefaultGravity { get; set; } = 5;
        public float InvertedGravity { get; set; } = -5;
        public bool IsInverted { get; private set; }

        private void Awake()
        {
            Initialise();
        }

        private void Initialise()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Invert()
        {
            IsInverted = !IsInverted;

           _rigidbody.gravityScale = IsInverted ? InvertedGravity : DefaultGravity;
        }
    }
}