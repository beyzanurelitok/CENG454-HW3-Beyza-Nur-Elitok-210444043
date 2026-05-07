using System;

namespace CoreBreach.Core
{
    public static class EventBus
    {
        public static event Action<float> OnCoreHealthChanged;
        public static event Action OnCoreDied;
        public static event Action<int> OnWaveStarted;
        public static event Action OnAllWavesCleared;

        public static void RaiseCoreHealthChanged(float healthPercent)
            => OnCoreHealthChanged?.Invoke(healthPercent);

        public static void RaiseCoreDied()
            => OnCoreDied?.Invoke();

        public static void RaiseWaveStarted(int waveNumber)
            => OnWaveStarted?.Invoke(waveNumber);

        public static void RaiseAllWavesCleared()
            => OnAllWavesCleared?.Invoke();
    }
}