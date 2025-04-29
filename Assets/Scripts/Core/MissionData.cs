using UnityEngine;

namespace IMP.Core
{
    public enum MissionType
    {
        BALL_LEFT,
        STAR_ALL_COLLECTED,
    }

    [CreateAssetMenu(fileName = "MissionData", menuName = "Scriptable Objects/MissionData")]
    public class MissionData : ScriptableObject
    {
        public MissionType Type;
        public int LeftCount;
    }
}

