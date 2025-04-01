using System.Collections.Generic;
using UnityEngine;

namespace IMP.Common
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField]
        private AudioStorage m_Storage;

        [SerializeField]
        private AudioSource m_AudioSource;

        private Dictionary<AudioType, AudioClip> m_AudioDict = new Dictionary<AudioType, AudioClip>();

        private void Start()
        {
            for (int i = 0; i < m_Storage.AudioDatas.Count; i++)
            {
                m_AudioDict.Add(m_Storage.AudioDatas[i].Type, m_Storage.AudioDatas[i].Clip);
            }
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
    }
}
