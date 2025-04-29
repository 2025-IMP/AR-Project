/// Owner: Dongjin Kuk
/// Description: This script defines the data type of the audio.

using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    TOUCH,
    Throw, 
    IMPACT,
    STAR,
    

}

[System.Serializable]
public struct AudioData
{
    public AudioType Type;
    public AudioClip Clip;
}

[CreateAssetMenu(fileName = "AudioStorage", menuName = "Scriptable Objects/AudioStorage")]
public class AudioStorage : ScriptableObject
{
    public List<AudioData> AudioDatas;
}
