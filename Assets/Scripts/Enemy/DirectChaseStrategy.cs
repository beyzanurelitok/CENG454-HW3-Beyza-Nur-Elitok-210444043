using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Enemy
{
    public class DirectChaseStrategy : MonoBehaviour, IMovementStrategy
    {
        public void Move(Transform enemy, Transform target, float speed)
        {
            Vector3 direction = (target.position - enemy.position).normalized;
            direction.y = 0f;
            enemy.position += direction * speed * Time.deltaTime;

            if (direction.sqrMagnitude > 0.01f)
                enemy.rotation = Quaternion.LookRotation(direction);
        }
    }
}