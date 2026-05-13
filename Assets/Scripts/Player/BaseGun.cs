using UnityEngine;
using CoreBreach.Interfaces;
using CoreBreach.Pool;

namespace CoreBreach.Player
{
    public class BaseGun : MonoBehaviour, IWeapon
    {
        [SerializeField] private ObjectPool projectilePool;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 0.3f;

        public float FireRate => fireRate;

        public virtual void Fire(Vector3 origin, Vector3 direction)
        {
            if (projectilePool == null || firePoint == null) return;

            GameObject bullet = projectilePool.Get(origin, Quaternion.LookRotation(direction));
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile?.Launch(direction);
        }
    }
}