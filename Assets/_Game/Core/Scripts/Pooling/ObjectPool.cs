using System;
using System.Collections.Generic;
using UnityEngine;
using _Game.Core.Scripts.Interfaces;
using Object = UnityEngine.Object;

namespace _Game.Core.Scripts.Pooling
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private Queue<T> _pool = new Queue<T>();
        private T _prefab;
        private Transform _parent;

        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            Initialise(initialSize);
        }

        private void Initialise(int size)
        {
            for (int i = 0; i < size; i++)
            {
                T obj = Object.Instantiate(_prefab, _parent);
                obj.OnReturnToPool = (poolable) => Return(poolable as T);
                obj.ResetObject();
                _pool.Enqueue(obj);
            }
        }

        public T Get(Action<T> configure = null)
        {
            if (_pool.Count == 0)
                Initialise(1);

            T obj = _pool.Dequeue();
            obj.Initialise();
            configure?.Invoke(obj);
            return obj;
        }

        private void Return(T obj)
        {
            if (obj == null) return;
            if (!_pool.Contains(obj))
            {
                obj.ResetObject();
                _pool.Enqueue(obj);
            }
        }
    }
}