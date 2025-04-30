/// Owner: Dongjin Kuk
/// Description: This script manages the audio system of the game.
/// The game plays an audio by using this script.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.Common
{
    public enum AudioCategory
    {
        BGM,
        SFX,
    }

    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField]
        private AudioStorage m_Storage;

        private float m_BGMVolume = 0.3f;
        private float m_SFXVolume = 1.0f;
        private Dictionary<AudioCategory, float> m_VolumeDict = new Dictionary<AudioCategory, float>();
        public Dictionary<AudioCategory, float> VolumeDict => m_VolumeDict;

        [SerializeField] private AudioSource m_BGMSource;
        [SerializeField] private AudioClip m_IntroBGM;

        private Dictionary<AudioType, AudioClip> m_AudioDict = new Dictionary<AudioType, AudioClip>();

        protected override void Awake()
        {
            base.Awake(); // Singleton 초기화

            // 효과음 초기화
            m_AudioDict.Clear();
            foreach (var data in m_Storage.AudioDatas)
            {
                m_AudioDict[data.Type] = data.Clip;
            }
        }

        private void Start()
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName.Contains("Game") || sceneName.Contains("Stage"))
            {
                m_AudioDict.Add(m_Storage.AudioDatas[i].Type, m_Storage.AudioDatas[i].Clip);
            }
            m_VolumeDict[AudioCategory.BGM] = m_BGMVolume;
            m_VolumeDict[AudioCategory.SFX] = m_SFXVolume;
        }

        public void PlayOneShot(AudioClip clip)
        {
            m_AudioSource.PlayOneShot(clip);
        }
        public void PlayOneShot(AudioType type)
        {
            AudioClip clip = m_AudioDict[type];
            PlayOneShot(clip);
        }

        public void SetVolume(AudioCategory category, float volume)
        {
            volume = Mathf.Clamp01(volume);
            m_VolumeDict[category] = volume;
        }
    }
}
