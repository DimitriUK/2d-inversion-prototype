using System;
using UnityEngine;

namespace _Game.Core.Scripts.Interfaces
{
    public interface IPoolable
    {
        void Initialise();
        void ResetObject();
        GameObject gameObject { get; }
        Action<IPoolable> OnReturnToPool { get; set; }
    }
}