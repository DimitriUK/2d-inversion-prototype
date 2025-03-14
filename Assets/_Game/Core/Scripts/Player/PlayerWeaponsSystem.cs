using System.Collections.Generic;
using _Game.Core.Scripts.Enums;
using _Game.Core.Scripts.Factory;
using _Game.Core.Scripts.Interfaces;
using _Game.Core.Scripts.Objects;
using UnityEngine;

namespace _Game.Core.Scripts.Player
{
    public class PlayerWeaponsSystem : MonoBehaviour
    {
        private PlayerAnimationSystem _animationSystem;
        private PlayerInputsSystem _inputsSystem;
        private PlayerMovementSystem _movementSystem;
    
        private readonly Dictionary<int, IWeapon> _weapons = new Dictionary<int, IWeapon>();
        private IWeapon _currentWeapon;
        
        [SerializeField] private WeaponFactory _weaponFactory;
        [SerializeField] private WeaponSO[] _weaponConfigs;
        [SerializeField] private Transform _weaponParent;

        [SerializeField] private List<GameObject> _weaponSprites;
        
        public void Initialise(PlayerAnimationSystem animationSystem, PlayerInputsSystem inputsSystem,
            PlayerMovementSystem movementSystem)
        {
            _animationSystem = animationSystem;
            _inputsSystem = inputsSystem;
            _movementSystem = movementSystem;
            
            InitialiseWeapons();
        }
        
        private void InitialiseWeapons()
        {
            foreach (WeaponSO config in _weaponConfigs)
            {
                IWeapon weapon = config.WeaponType == Weapons.Unarmed ? _weaponFactory.CreateUnarmed() : _weaponFactory.CreateWeapon(config);

                if (weapon != null)
                    RegisterWeapon(weapon);
            }
        }

        private void OnEnable()
        {
            _inputsSystem.OnSwitchWeaponPressed += SwitchWeapon;
        }
        
        private void OnDisable()
        {
            _inputsSystem.OnSwitchWeaponPressed -= SwitchWeapon;
        }
        
        public void Attack()
        {
            if (_currentWeapon == null)
            {
                Debug.LogWarning("Tried to attack! No equipped weapon");
                return;
            }

            if (_currentWeapon != null) 
                _currentWeapon.Attack();
        }

        public void Reload()
        {
            if (_currentWeapon is IRangedWeapon rangedWeapon)
                rangedWeapon.Reload();
        }

        /// <summary>
        /// TODO: Use this later to make different weapons have special attacks with special multipliers, etc.
        /// </summary>
        public void TryMeleeSpecial()
        {
            if (_currentWeapon is IMeleeWeapon meleeWeapon)
            {
                Debug.Log($"Sharp Damage: {meleeWeapon.SharpDamage}");
            }
        }

        private void RegisterWeapon(IWeapon weapon)
        {
            if (weapon == null) 
            {
                Debug.LogError("Tried to register null weapon.");
                return;
            }
    
            if (!_weapons.TryAdd(weapon.WeaponID, weapon))
            {
                Debug.LogError($"Weapon ID {weapon.WeaponID} already registered.");
                return;
            }

            if (weapon is IMovableWeapon rangedWeapon && _movementSystem != null)
            {
                rangedWeapon.InitialiseMovement(_movementSystem);
            }
    
            _weapons[weapon.WeaponID] = weapon;
        }

        private void SwitchWeapon(int weaponID)
        {
            if (_weapons.TryGetValue(weaponID, out IWeapon newWeapon))
            {
                _currentWeapon = newWeapon;
                _animationSystem.SetEquippedId(weaponID);
                SwitchWeaponSprites(weaponID);
            }
        }

        private void SwitchWeaponSprites(int weaponID)
        {
            for (int i = 0; i < _weaponSprites.Count; i++)
            {
                if (i == weaponID)
                {
                    _weaponSprites[i].SetActive(true);
                    continue;
                }
                
                _weaponSprites[i].SetActive(false);
            }
        }
    }
}