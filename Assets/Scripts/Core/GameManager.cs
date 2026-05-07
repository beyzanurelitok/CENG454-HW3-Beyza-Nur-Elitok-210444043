using UnityEngine;
using CoreBreach.Core;

namespace CoreBreach.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private GameState _currentState;
        public GameState CurrentState => _currentState;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            EventBus.OnCoreDied += HandleCoreDied;
            EventBus.OnAllWavesCleared += HandleAllWavesCleared;
        }

        private void OnDisable()
        {
            EventBus.OnCoreDied -= HandleCoreDied;
            EventBus.OnAllWavesCleared -= HandleAllWavesCleared;
        }

        private void Start()
        {
            SetState(GameState.Playing);
        }

        private void HandleCoreDied() => SetState(GameState.Lost);
        private void HandleAllWavesCleared() => SetState(GameState.Won);

        private void SetState(GameState newState)
        {
            _currentState = newState;
            Debug.Log($"[GameManager] State: {newState}");
        }
    }
}