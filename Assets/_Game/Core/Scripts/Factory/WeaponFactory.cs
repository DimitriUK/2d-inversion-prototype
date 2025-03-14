using _Game.Core.Scripts.Combat;
using _Game.Core.Scripts.Interfaces;
using _Game.Core.Scripts.Objects;

namespace _Game.Core.Scripts.Factory
{
    using UnityEngine;

    public class WeaponFactory : MonoBehaviour
    {
        [SerializeField] private Transform _weaponParent;
    
        public IWeapon CreateWeapon(WeaponSO config)
        {
            if (config.Prefab == null)
            {
                Debug.LogError($"Missing prefab in {config.name}");
                return null;
            }

            GameObject weaponObj = Instantiate(config.Prefab, _weaponParent);
            IWeapon weapon = weaponObj.GetComponent<IWeapon>();
        
            if (weapon == null)
            {
                Debug.LogError($"Prefab {config.Prefab.name} doesn't implement IWeapon");
                Destroy(weaponObj);
                return null;
            }

            return weapon;
        }

        public IWeapon CreateUnarmed()
        {
            return new Unarmed();
        }
    }
}