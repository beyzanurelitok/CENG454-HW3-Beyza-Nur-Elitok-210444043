using UnityEngine;

namespace CoreBreach.Interfaces
{
    public interface IWeapon
    {
        void Fire(Vector3 origin, Vector3 direction);
        float FireRate { get; }
    }
}