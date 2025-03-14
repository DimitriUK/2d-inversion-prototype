using _Game.Core.Scripts.Interfaces;
using _Game.Core.Scripts.Objects;
using _Game.Core.Scripts.Pooling;
using UnityEngine;

namespace _Game.Core.Scripts.Combat
{
    public abstract class RangedWeaponBase : MonoBehaviour, IRangedWeapon
    {
        [SerializeField] protected WeaponSO _config;
        [SerializeField] protected Transform _firePoint;
    
        protected ObjectPool<Bullet> _projectilePool;
        protected int _currentAmmo;
        protected float _lastFireTime;

        public int CurrentAmmo => _currentAmmo;
        public int MaxAmmo => _config.MaxAmmo;
        public float AttackCooldown => _config.Cooldown;
        public bool CanAttack => Time.time > _lastFireTime + AttackCooldown && _currentAmmo > 0;
        public int WeaponID => _config.WeaponId;

        protected virtual void Awake()
        {
            _currentAmmo = MaxAmmo;
            InitializePool();
        }
        
        protected virtual void InitializePool()
        {
            Bullet prefab = Resources.Load<Bullet>(nameof(Bullet));
            _projectilePool = new ObjectPool<Bullet>(prefab, 10, transform);
        }

        public virtual void Attack()
        {
            if (!CanAttack) return;
        
            Bullet bullet = _projectilePool.Get();
            ConfigureBullet(bullet);
            _currentAmmo--;
            _lastFireTime = Time.time;
            bullet.transform.parent = null;
        }

        protected abstract void ConfigureBullet(Bullet bullet);
        public virtual void Reload() => _currentAmmo = MaxAmmo;
    }
}