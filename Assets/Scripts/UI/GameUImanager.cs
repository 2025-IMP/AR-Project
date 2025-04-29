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

        [SerializeField]
        private UIBallSelection m_BallSelection;
        public UIBallSelection BallSelection => m_BallSelection;

        [SerializeField]
        private UIStarCollection m_StarCollection;
        public UIStarCollection StarCollection => m_StarCollection;

        public GameObject gameOverPanel;
        public GameObject stageClearPanel;

        private void Awake()
        {
            s_Instance = this;
        }

        public void Initialize()
        {
            m_BallSelection.Initialize();
            m_StarCollection.Initialize();
        }
    }
}
