// StageData.cs
using System.Collections.Generic;
using UnityEngine;

namespace IMP.Core
{
    [CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
    public class StageData : ScriptableObject
    {
        public List<Ball> BallPrefabs;
        public Structure StructurePrefab;
        public int BallCount;
    }
}
