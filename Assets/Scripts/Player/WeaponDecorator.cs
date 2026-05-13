using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Player
{
    public abstract class WeaponDecorator : MonoBehaviour, IWeapon
    {
        protected IWeapon _wrapped;

        public virtual float FireRate => _wrapped.FireRate;

        public void SetWeapon(IWeapon weapon)
        {
            _wrapped = weapon;
        }

        public abstract void Fire(Vector3 origin, Vector3 direction);
    }
}