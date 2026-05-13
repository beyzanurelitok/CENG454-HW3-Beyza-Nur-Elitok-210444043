using UnityEngine;
using CoreBreach.Interfaces;
using CoreBreach.Objective;

namespace CoreBreach.Enemy
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class EnemyBase : MonoBehaviour, IDamageable, IPoolable
    {
        [SerializeField] private float maxHealth = 30f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float damagePerSecond = 10f;

        private float _currentHealth;
        private Transform _coreTransform;
        private IMovementStrategy _movementStrategy;

        public bool IsAlive => _currentHealth > 0f;

        private void Awake()
        {
            _movementStrategy = GetComponent<IMovementStrategy>();
        }

        private void Start()
        {
            GameObject core = GameObject.FindWithTag("Core");
            if (core != null)
                _coreTransform = core.transform;
        }

        private void Update()
        {
            if (!IsAlive || _coreTransform == null) return;

            _movementStrategy?.Move(transform, _coreTransform, moveSpeed);

            float dist = Vector3.Distance(transform.position, _coreTransform.position);
            if (dist < 1.5f)
            {
                IDamageable core = _coreTransform.GetComponent<IDamageable>();
                core?.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }

        public void TakeDamage(float amount)
        {
            if (!IsAlive) return;
            _currentHealth -= amount;
            if (!IsAlive)
                ReturnToPool();
        }

        public void OnSpawnFromPool()
        {
            _currentHealth = maxHealth;
            gameObject.SetActive(true);
        }

        public void OnReturnToPool()
        {
            gameObject.SetActive(false);
        }

        private void ReturnToPool()
        {
            WaveManager waveManager = FindFirstObjectByType<WaveManager>();
            waveManager?.OnEnemyDied();
            OnReturnToPool();
        }
    }
}