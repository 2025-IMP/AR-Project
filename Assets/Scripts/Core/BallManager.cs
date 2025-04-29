/// Owner: Dongjin Kuk
/// Description: It stores ball prefabs with its' type.

using System.Collections.Generic;
using UnityEngine;

namespace IMP.Core
{
    [System.Serializable]
    public struct BallInfo
    {
        public BallType Type;
        public Ball Prefab;
        public Sprite Sprite;
    }

    public class BallManager : Singleton<BallManager>
    {
        [SerializeField]
        private BallInfo[] m_BallStorage;

        public static Dictionary<BallType, BallInfo> BallInfoDict = new Dictionary<BallType, BallInfo>();

        private void Start()
        {
            foreach (var ballInfo in m_BallStorage)
            {
                BallInfoDict[ballInfo.Type] = ballInfo;
            }
        }

        public static Ball GetBallPrefab(BallType type)
        {
            return BallInfoDict[type].Prefab;
        }
        public static Sprite GetBallSprite(BallType type)
        {
            return BallInfoDict[type].Sprite;
        }
    }
}
