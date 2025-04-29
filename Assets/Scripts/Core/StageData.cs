/// Owner: Donghun Lee
/// Description: It defines the data of each stage.

using System.Collections.Generic;
using UnityEngine;

namespace IMP.Core
{
    [System.Serializable]
    public struct BallData
    {
        public BallType Type;
        public int Count;
    }

    [CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
    public class StageData : ScriptableObject
    {
        public List<BallData> BallDatas;
        public Structure StructurePrefab;
    }
}
