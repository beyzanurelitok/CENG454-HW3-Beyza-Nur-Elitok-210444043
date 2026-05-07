using UnityEngine;

namespace CoreBreach.Interfaces
{
    public interface IMovementStrategy
    {
        void Move(Transform enemy, Transform target, float speed);
    }
}