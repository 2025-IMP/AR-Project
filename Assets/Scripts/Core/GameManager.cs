/// Owner: Dongjin Kuk
/// Description: This script manages the game logic.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using TrackableType = UnityEngine.XR.ARSubsystems.TrackableType;
using UnityEngine.SceneManagement;
using IMP.UI;
using System.Linq;
using System.Collections;

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
        private Dictionary<BallType, int> m_BallDict = new Dictionary<BallType, int>();
        public Dictionary<BallType, int> BallDict => m_BallDict;

        private GameState m_State = GameState.READY;
        public GameState State => m_State;

        private int m_StarCount = 0;
        public int StarCount => m_StarCount;

        private void Awake()
        {
            s_Instance = this;
        }

        private void Start()
        {
            Initialize();
            PrepareStage();
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
            m_BallDict.Clear();
            m_StarCount = 0;

            m_Slingshot.Initialize();
            GameUIManager.Instance.Initialize();
        }

        private void PrepareStage()
        {
            m_StageData = StageManager.Instance.CurrStageData;
            m_StageData ??= m_DefaultStageData;

            foreach (var ballData in m_StageData.BallDatas)
            {
                m_BallDict[ballData.Type] = ballData.Count;
            }
            GameUIManager.Instance.BallSelection.Config(m_BallDict);
            GameUIManager.Instance.BallSelection.OnBallCellPressed(0);

            int starCount = m_StageData.StructurePrefab.StarRoot.childCount;
            GameUIManager.Instance.StarCollection.Config(starCount);
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
                StartCoroutine(SetThrowableCoroutine());
            }
        }

        private IEnumerator SetThrowableCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            m_Slingshot.Throwable = true;
        }

        public void SpawnBall(BallType type)
        {
            Ball ballPrefab = BallManager.GetBallPrefab(type);
            Ball ball = Instantiate(ballPrefab);

            m_Slingshot.SetCurrentBall(ball);
        }

        public void SyncBallCount()
        {
            GameUIManager.Instance.BallSelection.Synchronize(m_BallDict);
        }

        public void ReduceBallCount(BallType ballType)
        {
            int ballCount = m_BallDict[ballType];
            ballCount -= 1;
            m_BallDict[ballType] = ballCount;

            GameUIManager.Instance.BallSelection.Synchronize(m_BallDict);

            if (ballCount == 0)
            {
                bool hasBall = false;
                foreach (var ballData in m_BallDict)
                {
                    if (ballData.Value <= 0) continue;

                    int cellIndex = GameUIManager.Instance.BallSelection.Cells.FindIndex(cell => cell.BallType == ballData.Key);
                    GameUIManager.Instance.BallSelection.OnBallCellPressed(cellIndex);
                    hasBall = true;

                    Debug.Log($"cellIndex: {cellIndex}");

                    break;
                }

                if (!hasBall)
                {
                    EndGame();
                }
            }
            else
            {
                SpawnBall(ballType);
            }
        }

        public void CollectStar()
        {
            m_StarCount += 1;
            GameUIManager.Instance.StarCollection.SetStarsActive(m_StarCount);
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
