using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Enemy
{
    public class FlankingStrategy : MonoBehaviour, IMovementStrategy
    {
        private float _flankAngle = 45f;
        private float _angleTimer = 0f;
        private float _angleChangeDuration = 2f;

        public void Move(Transform enemy, Transform target, float speed)
        {
            _angleTimer += Time.deltaTime;
            if (_angleTimer >= _angleChangeDuration)
            {
                _flankAngle = Random.Range(-60f, 60f);
                _angleTimer = 0f;
            }

            Vector3 dirToTarget = (target.position - enemy.position).normalized;
            dirToTarget.y = 0f;

            Vector3 flankedDir = Quaternion.Euler(0f, _flankAngle, 0f) * dirToTarget;

            enemy.position += flankedDir * speed * Time.deltaTime;

            if (flankedDir.sqrMagnitude > 0.01f)
                enemy.rotation = Quaternion.LookRotation(flankedDir);
        }
    }
}