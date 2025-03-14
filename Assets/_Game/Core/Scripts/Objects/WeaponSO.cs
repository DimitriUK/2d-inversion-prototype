using _Game.Core.Scripts.Enums;
using UnityEngine;

namespace _Game.Core.Scripts.Objects
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        public Weapons WeaponType;
        public GameObject Prefab;
        public float Cooldown;
        public int MaxAmmo;
        public int WeaponId;
        public float XFiringOffset;
    }
}