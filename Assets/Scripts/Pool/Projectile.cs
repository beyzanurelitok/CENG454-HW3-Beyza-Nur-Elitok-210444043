using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Pool
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] private float speed = 15f;
        [SerializeField] private float damage = 20f;
        [SerializeField] private float lifetime = 3f;

        private Rigidbody _rb;
        private ObjectPool _pool;
        private float _timer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Init(ObjectPool pool)
        {
            _pool = pool;
        }

        public void OnSpawnFromPool()
        {
            _timer = 0f;
            gameObject.SetActive(true);
        }

        public void OnReturnToPool()
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void Launch(Vector3 direction)
        {
            _rb.linearVelocity = direction.normalized * speed;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= lifetime)
                ReturnToPool();
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null && other.CompareTag("Enemy"))
            {
                damageable.TakeDamage(damage);
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            if (_pool != null)
                _pool.Return(gameObject);
            else
                gameObject.SetActive(false);
        }
    }
}