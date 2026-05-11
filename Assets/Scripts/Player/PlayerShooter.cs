using UnityEngine;
using CoreBreach.Pool;

namespace CoreBreach.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private ObjectPool projectilePool;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 0.3f;

        private float _fireTimer = 0f;

        private void Update()
        {
            _fireTimer += Time.deltaTime;

            if (Input.GetMouseButton(0) && _fireTimer >= fireRate)
            {
                _fireTimer = 0f;
                Shoot();
            }
        }

        private void Shoot()
        {
            if (projectilePool == null || firePoint == null)
            {
                Debug.Log("Pool veya FirePoint null!");
                return;
            }

            Debug.Log("Shoot çaðrýldý!");
            GameObject bullet = projectilePool.Get(firePoint.position, firePoint.rotation);
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile?.Launch(transform.forward);
        }
    }
}