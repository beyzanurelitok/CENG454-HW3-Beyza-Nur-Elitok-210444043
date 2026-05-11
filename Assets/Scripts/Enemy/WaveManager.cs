using System.Collections;
using UnityEngine;
using CoreBreach.Core;

namespace CoreBreach.Enemy
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int totalWaves = 3;
        [SerializeField] private float timeBetweenWaves = 5f;

        private int _currentWave = 0;
        private int _enemiesAlive = 0;

        private void Start()
        {
            StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            while (_currentWave < totalWaves)
            {
                _currentWave++;
                EventBus.RaiseWaveStarted(_currentWave);

                int enemyCount = _currentWave * 3;
                _enemiesAlive = enemyCount;

                for (int i = 0; i < enemyCount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(0.5f);
                }

                yield return new WaitUntil(() => _enemiesAlive <= 0);
                yield return new WaitForSeconds(timeBetweenWaves);
            }

            EventBus.RaiseAllWavesCleared();
        }

        private void SpawnEnemy()
        {
            if (spawnPoints.Length == 0) return;

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
            if (enemyBase != null)
                enemyBase.OnSpawnFromPool();
        }

        public void OnEnemyDied()
        {
            _enemiesAlive--;
        }
    }
}