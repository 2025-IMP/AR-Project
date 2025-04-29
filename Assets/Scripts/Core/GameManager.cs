/// Owner: Dongjin Kuk
/// Description: This script manages the game logic.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using TrackableType = UnityEngine.XR.ARSubsystems.TrackableType;
using UnityEngine.SceneManagement;
using System;
using IMP.UI;

namespace IMP.Core
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            READY,
            BUILT,
            CLEAR,
        }

        private static GameManager s_Instance;
        public static GameManager Instance => s_Instance;

        [SerializeField]
        private ARRaycastManager m_ARRaycastManager;
        private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

        [SerializeField]
        private StageData m_DefaultStageData;
        private StageData m_StageData;
        public StageData StageData => m_StageData;

        [SerializeField] private Slingshot m_Slingshot;

        private Structure m_Structure = null;
        private Queue<Ball> m_BallQueue = new Queue<Ball>();

        private GameState m_State = GameState.READY;
        public GameState State => m_State;

        public Action OnStarCollected;

        public void OnStarCollect()
        {
            
        }

        void OnEnable()
        {
            OnStarCollected += OnStarCollect;
        }

        void OnDisable()
        {
            OnStarCollected -= OnStarCollect;
        }

        private void Awake()
        {
            s_Instance = this;
        }

        private void Start()
        {
            Initialize();
            PrepareStage();
            PrepareBalls();
        }

        private void Update()
        {
            var touches = Touch.activeTouches;

            if (touches.Count == 1 && touches[0].phase == TouchPhase.Began)
            {
                if (m_State == GameState.READY)
                {
                    BuildStructure(touches[0].screenPosition);
                }
            }
        }

        public void Initialize()
        {
            m_State = GameState.READY;
            m_Structure = null;
            m_BallQueue.Clear();

            m_Slingshot.Initialize();
        }

        private void PrepareStage()
        {
            m_StageData = StageManager.Instance.CurrStageData;
            m_StageData ??= m_DefaultStageData;
        }

        private void BuildStructure(Vector2 screenPos)
        {
            if (m_ARRaycastManager.Raycast(screenPos, m_Hits, TrackableType.PlaneWithinPolygon))
            {
                m_Structure = Instantiate(m_StageData.StructurePrefab);
                m_Structure.Initialize();
                m_Structure.transform.position = m_Hits[0].pose.position;
                m_Structure.transform.rotation = Camera.main.transform.rotation;

                float dist = m_Hits[0].distance;
                m_Structure.transform.localScale = new Vector3(
                    dist / 0.23f,
                    dist / 0.23f,
                    dist / 0.23f
                );

                m_State = GameState.BUILT;
            }
        }

        private void PrepareBalls()
        {
            for (int i = 0; i < m_StageData.BallPrefabs.Count; i++)
            {
                m_BallQueue.Enqueue(m_StageData.BallPrefabs[i]);
            }

            GameUIManager.Instance.SetBallCount(m_BallQueue.Count);
        }

        public void SpawnNextBall()
        {
            if (m_BallQueue.Count > 0)
            {
                Ball nextBallPrefab = m_BallQueue.Dequeue();
                m_Slingshot.SetCurrentBall(nextBallPrefab);
            }
            else
            {
                m_Slingshot.SetCurrentBall(null);
                EndGame();
            }

            GameUIManager.Instance.SetBallCount(m_BallQueue.Count);
        }

        private void EndGame()
        {
            m_State = GameState.CLEAR;
            GameUIManager.Instance.gameOverPanel.SetActive(true);

        }
        public void ToStageScene()
        {
            SceneManager.LoadSceneAsync("StageScene");
        }
    }
}
