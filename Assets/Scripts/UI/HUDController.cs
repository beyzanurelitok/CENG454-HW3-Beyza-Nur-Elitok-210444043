using UnityEngine;
using UnityEngine.UI;
using CoreBreach.Core;

namespace CoreBreach.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Slider coreHealthSlider;
        [SerializeField] private Text waveLabel;
        [SerializeField] private Text statusText;

        private void OnEnable()
        {
            EventBus.OnCoreHealthChanged += OnCoreHealthChanged;
            EventBus.OnWaveStarted += OnWaveStarted;
            EventBus.OnCoreDied += OnCoreDied;
            EventBus.OnAllWavesCleared += OnAllWavesCleared;
        }

        private void OnDisable()
        {
            EventBus.OnCoreHealthChanged -= OnCoreHealthChanged;
            EventBus.OnWaveStarted -= OnWaveStarted;
            EventBus.OnCoreDied -= OnCoreDied;
            EventBus.OnAllWavesCleared -= OnAllWavesCleared;
        }

        private void OnCoreHealthChanged(float percent)
        {
            if (coreHealthSlider != null)
                coreHealthSlider.value = percent;
        }

        private void OnWaveStarted(int wave)
        {
            if (waveLabel != null)
                waveLabel.text = $"Wave {wave}";
        }

        private void OnCoreDied()
        {
            if (statusText != null)
                statusText.text = "CORE BREACHED - YOU LOSE";
        }

        private void OnAllWavesCleared()
        {
            if (statusText != null)
                statusText.text = "ALL WAVES CLEARED - YOU WIN";
        }
    }
}