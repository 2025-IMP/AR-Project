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
            m_BGMSlider.value = AudioManager.Instance.VolumeDict[AudioCategory.BGM];
            m_SFXSlider.value = AudioManager.Instance.VolumeDict[AudioCategory.SFX];
        }

        public void OnBGMSliderChanged()
        {
            AudioManager.Instance.SetVolume(AudioCategory.BGM, m_BGMSlider.value);
        }
        public void OnSFXSliderChanged()
        {
            AudioManager.Instance.SetVolume(AudioCategory.SFX, m_SFXSlider.value);
        }

        public void OnBackButtonPressed()
        {
            gameObject.SetActive(false);
        }
    }
}
