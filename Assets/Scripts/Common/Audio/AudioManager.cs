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

        [SerializeField]
        private AudioSource m_AudioSource;

        private float m_BGMVolume = 0.3f;
        private float m_SFXVolume = 1.0f;
        private Dictionary<AudioCategory, float> m_VolumeDict = new Dictionary<AudioCategory, float>();
        public Dictionary<AudioCategory, float> VolumeDict => m_VolumeDict;

        [SerializeField] private AudioSource m_BGMSource;
        public AudioSource BGMSource => m_BGMSource;

        [SerializeField] private AudioClip m_IntroBGM;
        public AudioClip IntroBGM => m_IntroBGM;

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
            m_VolumeDict[AudioCategory.BGM] = m_BGMVolume;
            m_VolumeDict[AudioCategory.SFX] = m_SFXVolume;

            m_BGMSource.volume = m_BGMVolume;
            m_AudioSource.volume = m_SFXVolume;
        }

        public void PlayBGM(AudioClip clip)
        {
            m_BGMSource.clip = clip;
            m_BGMSource.Play();
        }

        public void PlayOneShot(AudioClip clip)
        {
            m_AudioSource.volume = m_SFXVolume;
            m_AudioSource.PlayOneShot(clip);
        }
        public void PlayOneShot(AudioType type)
        {
            m_AudioSource.volume = m_SFXVolume;
            AudioClip clip = m_AudioDict[type];
            PlayOneShot(clip);
        }

        public void SetVolume(AudioCategory category, float volume)
        {
            volume = Mathf.Clamp01(volume);
            m_VolumeDict[category] = volume;

            if (category == AudioCategory.BGM)
            {
                m_BGMSource.volume = volume;
            }
        }
    }
}
