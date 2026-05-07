using UnityEngine;
using CoreBreach.Core;
using CoreBreach.Interfaces;

namespace CoreBreach.Objective
{
    public class CoreHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 100f;

        private float _currentHealth;

        public bool IsAlive => _currentHealth > 0f;

        private void Start()
        {
            _currentHealth = maxHealth;
            EventBus.RaiseCoreHealthChanged(1f);
        }

        public void TakeDamage(float amount)
        {
            if (!IsAlive) return;

            _currentHealth = Mathf.Max(0f, _currentHealth - amount);
            EventBus.RaiseCoreHealthChanged(_currentHealth / maxHealth);

            if (!IsAlive)
                EventBus.RaiseCoreDied();
        }

        // Geçici test metodu - sonra silinecek
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TakeDamage(25f);
        }
    }
}