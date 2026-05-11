using System.Collections.Generic;
using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize = 10;

        private Stack<GameObject> _pool = new Stack<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = CreateNew();
                _pool.Push(obj);
            }
        }

        private GameObject CreateNew()
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            return obj;
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject obj = _pool.Count > 0 ? _pool.Pop() : CreateNew();
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            IPoolable poolable = obj.GetComponent<IPoolable>();
            poolable?.OnSpawnFromPool();

            return obj;
        }

        public void Return(GameObject obj)
        {
            IPoolable poolable = obj.GetComponent<IPoolable>();
            poolable?.OnReturnToPool();

            _pool.Push(obj);
        }
    }
}