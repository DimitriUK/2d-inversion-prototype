using System;
using _Game.Core.Scripts.Interfaces;
using UnityEngine;

namespace _Game.Core.Scripts.Combat
{
    public class Bullet : MonoBehaviour, IPoolable, IInvertable
    {
        private Rigidbody2D _rigidbody;
        
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 2f;
        
        [SerializeField] private LayerMask _layerMask;
        
        public float DefaultGravity { get; set; } = 5;
        public float InvertedGravity { get; set; } = -5;
        public bool IsInverted { get; private set; }

        public Action<IPoolable> OnReturnToPool { get; set; }
        
        private float _timer;
        
        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void Initialise()
        {
            gameObject.SetActive(true);
            _timer = _lifeTime;
        }
        
        private void Update()
        {
            CheckTimerAndReturnToPool();
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer != _layerMask)
                return;

            OnReturnToPool?.Invoke(this);
        }
        
        public void Invert()
        {
            IsInverted = !IsInverted;

            _rigidbody.gravityScale = IsInverted ? InvertedGravity : DefaultGravity;
        }

        public void ResetObject()
        {
            if (IsInverted)
                Invert();
            
            _rigidbody.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);

        }

        public void SetDirection(Vector2 direction)
        {
            _rigidbody.linearVelocity = direction * _speed;
        }

        private void CheckTimerAndReturnToPool()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0) 
                OnReturnToPool?.Invoke(this);
        }
    }
}