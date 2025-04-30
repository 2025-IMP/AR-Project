using IMP.Common;
using UnityEngine;
using UnityEngine.UI;

namespace IMP.UI
{
    public class UISettings : MonoBehaviour
    {
        [SerializeField] private Slider m_BGMSlider;
        [SerializeField] private Slider m_SFXSlider;

        private void OnEnable()
        {
            m_BGMSlider.value = AudioManager.Instance.VolumeDict[AudioCategory.BGM] * 10;
            m_SFXSlider.value = AudioManager.Instance.VolumeDict[AudioCategory.SFX] * 10;
        }

        public void OnBGMSliderChanged()
        {
            AudioManager.Instance.SetVolume(AudioCategory.BGM, m_BGMSlider.value * 0.1f);
        }
        public void OnSFXSliderChanged()
        {
            AudioManager.Instance.SetVolume(AudioCategory.SFX, m_SFXSlider.value * 0.1f);
        }

        public void OnBackButtonPressed()
        {
            gameObject.SetActive(false);
        }
    }
}
