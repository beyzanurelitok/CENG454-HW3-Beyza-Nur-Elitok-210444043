using UnityEngine;

namespace CoreBreach.Player
{
    public class DoubleShotDecorator : WeaponDecorator
    {
        [SerializeField] private float spreadAngle = 15f;

        public override void Fire(Vector3 origin, Vector3 direction)
        {
            // Orijinal silahý ateþle
            _wrapped.Fire(origin, direction);

            // Ýkinci mermiyi ateþle
            Vector3 spreadDir = Quaternion.Euler(0f, spreadAngle, 0f) * direction;
            _wrapped.Fire(origin, spreadDir);
        }
    }
}