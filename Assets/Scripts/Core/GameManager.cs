using System.Collections.Generic;
using IMP.UI;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using TrackableType = UnityEngine.XR.ARSubsystems.TrackableType;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Collections;

namespace IMP.Core
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            READY,
            BUILT,
            PLAING,
            CLEAR,
            FAIL,
        }
        
        private static GameManager s_Instance;
        public static GameManager Instance => s_Instance;

        [SerializeField] private ARRaycastManager m_ARRaycastManager;
        private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

        [SerializeField] private StageData StageData1;
        [SerializeField] private StageData StageData2;
        [SerializeField] private StageData StageData3;        
        private StageData m_StageData; // 추가 한 코드
        public StageData StageData => m_StageData;

        [SerializeField] private Transform m_BallSpawnPoint;

        [SerializeField] private Slingshot m_Slingshot;

        private Structure m_Structure = null;
        private Queue<Ball> m_BallQueue = new Queue<Ball>();
        private Ball m_CurrentBall;

        private GameState m_State = GameState.READY;
        public GameState State => m_State;

        [SerializeField] private Structure stagePrefab1;
        [SerializeField] private Structure stagePrefab2;
        [SerializeField] private Structure stagePrefab3;
        private Structure m_StructurePrefab;
        [SerializeField] private GameUImanager gameUiManager;
        private int ballCount;

        private void Awake()
        {
            s_Instance = this;
        }

        private void Start()
        {
            m_StageData = StageManager.Instance.CurrStageData;
            Initialize();
            PrepareStage();
            PrepareBalls();
            Debug.Log("asdfgrfsag");
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
            m_CurrentBall = null;
        }

        private void PrepareStage()
        {
            m_StructurePrefab = m_StageData.StructurePrefab;
        }

        private void BuildStructure(Vector2 screenPos)
        {
            if (m_ARRaycastManager.Raycast(screenPos, m_Hits, TrackableType.PlaneWithinPolygon))
            {
                m_Structure = Instantiate(m_StructurePrefab);
                m_Structure.Initialize();
                m_Structure.transform.position = m_Hits[0].pose.position;
                m_Structure.transform.rotation = Camera.main.transform.rotation;

                float dist = m_Hits[0].distance;
                m_Structure.transform.localScale = new Vector3(
                    dist / 0.23f,
                    dist / 0.23f,
                    dist / 0.23f
                );

                StartCoroutine(ChangeGameStateNextFrame(GameState.BUILT));
                SpawnNextBall();
            }
        }

        private IEnumerator ChangeGameStateNextFrame(GameState state)
        {
            yield return new WaitForEndOfFrame();

            m_State = state;
        }

        private void PrepareBalls()
        {
            for (int i = 0; i < m_StageData.BallPrefabs.Count; i++)
            {
                m_BallQueue.Enqueue(m_StageData.BallPrefabs[i]);
                Debug.Log(""+m_BallQueue.Count);
            }

            gameUiManager.OnBallCountChanged?.Invoke(m_BallQueue.Count);
        }


        public void SpawnNextBall()
        {
            if (m_BallQueue.Count > 0)
            {
                m_CurrentBall = m_BallQueue.Dequeue();
                m_Slingshot.SetCurrentBall(m_CurrentBall);
            }
            else
            {
                m_Slingshot.SetCurrentBall(null);
                EndGame();
            }

            gameUiManager.OnBallCountChanged?.Invoke(m_BallQueue.Count);
        }
        private void EndGame()
        {
            m_State = GameState.CLEAR;
            Debug.Log("모든 공을 사용했습니다! 게임 끝!");

            //일단 게임 오버만
            gameUiManager.gameOverPanel.SetActive(true);
           
        }
        public void ToStageScene(){
            SceneManager.LoadSceneAsync("StageScene");
        }
    }
}
