/// Owner: Minseong Kim
/// Description: It manages the UIs in the game.

using System;
using IMP.Core;
using TMPro;
using UnityEngine;

namespace IMP.UI
{
    public class GameUIManager : MonoBehaviour
    {
        private static GameUIManager s_Instance;
        public static GameUIManager Instance => s_Instance;

        public GameObject gameOverPanel;
        public GameObject stageClearPanel;
        public TMP_Text ballCountText;
        public TMP_Text starCountText;

        public Action<int> OnBallCountChanged;

        void Awake()
        {
            s_Instance = this;
        }

        public void SetBallCount(int ballCount)
        {
            ballCountText.text = $"Ball {ballCount}/{GameManager.Instance.StageData.BallPrefabs.Count}";
        }

        public void SetStarCount(int starCount)
        {

        }
    }
}
