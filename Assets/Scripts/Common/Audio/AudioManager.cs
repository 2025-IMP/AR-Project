/// Owner: Dongjin Kuk
/// Description: This script manages the audio system of the game.
/// The game plays an audio by using this script.

using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField]
        private AudioSource m_AudioSource;

        private Dictionary<AudioType, AudioClip> m_AudioDict = new Dictionary<AudioType, AudioClip>();

        private void Start()
        {
            for (int i = 0; i < m_Storage.AudioDatas.Count; i++)
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
