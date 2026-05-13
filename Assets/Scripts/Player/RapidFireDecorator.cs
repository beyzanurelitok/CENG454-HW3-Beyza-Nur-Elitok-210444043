using UnityEngine;

namespace CoreBreach.Player
{
    public class RapidFireDecorator : WeaponDecorator
    {
        [SerializeField] private float fireRateMultiplier = 2f;

        public override float FireRate => _wrapped.FireRate / fireRateMultiplier;

        public override void Fire(Vector3 origin, Vector3 direction)
        {
            _wrapped.Fire(origin, direction);
        }
    }
}