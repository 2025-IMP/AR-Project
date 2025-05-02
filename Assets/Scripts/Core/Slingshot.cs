/// Owner: Dongjin Kuk
/// Description: This is the script for the slingshot. It throws the current ball.

using IMP.Common;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace IMP.Core
{
    public class Slingshot : MonoBehaviour
    {
        [SerializeField] private TrajectoryPrediction m_TrajPrediction;
        [SerializeField] private Transform m_BallRoot;

        [SerializeField] private Ball m_BallPrefab;

        [SerializeField] private float m_HorzDeltaBoundary = 300f;
        [SerializeField] private float m_VertDeltaBoundary = 500f;
        [SerializeField] private float m_HorzPowerBoundary = 30f;
        [SerializeField] private float m_VertPowerBoundary = 30f;

        private Ball m_CurrentBall;

        private Vector2 m_TouchPos;
        private Vector2 m_DeltaPos;
        private Vector3 m_Force;

        private bool m_Touching = false;

        [HideInInspector]
        public bool Throwable = false;

        public void Initialize()
        {
            m_CurrentBall = null;
            Throwable = false;
            m_TrajPrediction.StopSimulation();
        }

        private void Update()
        {
            if (!Throwable) return;
            if (m_CurrentBall == null) return;

            HandleInput();

            if (m_Touching)
            {
                Vector3 force = m_BallRoot.rotation * m_Force;
                m_TrajPrediction.Simulate(m_CurrentBall, m_BallRoot.position, force);
            }
            else
            {
                m_TrajPrediction.StopSimulation();
            }
        }

        private void HandleInput()
        {
            if (m_CurrentBall == null) return;

            var touches = Touch.activeTouches;

            if (touches.Count == 1 && !PointBlocker.IsOverUI(touches[0].screenPosition) && touches[0].phase == TouchPhase.Began)
            {
                m_TouchPos = touches[0].screenPosition;
                m_Touching = true;
            }
            else if (touches.Count == 1 && m_Touching && touches[0].phase == TouchPhase.Stationary)
            {
                m_DeltaPos = m_TouchPos - touches[0].screenPosition;
                CalculateForce();
            }
            else if (
                touches.Count == 1 && m_Touching
                && touches[0].phase == TouchPhase.Ended
            )
            {
                m_DeltaPos = m_TouchPos - touches[0].screenPosition;
                CalculateForce();

                m_Touching = false;
                Throw();
            }
        }

        private void CalculateForce()
        {
            float horzRatio = Mathf.Abs(m_DeltaPos.x) / m_HorzDeltaBoundary;
            horzRatio = Mathf.Min(horzRatio, 1f);
            float horzPower =
                Mathf.Lerp(0f, m_HorzPowerBoundary, horzRatio) * Mathf.Sign(m_DeltaPos.x);

            float vertRatio = Mathf.Abs(m_DeltaPos.y) / m_VertDeltaBoundary;
            vertRatio = Mathf.Min(vertRatio, 1f);
            float vertPower =
                Mathf.Lerp(0f, m_VertPowerBoundary, vertRatio) * Mathf.Sign(m_DeltaPos.y);

            m_Force = new Vector3(horzPower, 0.2f, vertPower);
        }

        public void SetCurrentBall(Ball ball)
        {
            if (ball == null)
            {
                m_CurrentBall = null;
                return;
            }

            if (m_CurrentBall != null)
            {
                Destroy(m_CurrentBall.gameObject);
            }

            m_CurrentBall = ball;
            ball.transform.SetParent(m_BallRoot);
            ball.transform.position = m_BallRoot.position;
        }

        /// <summary>
        /// Throw the current ball. It invokes when the player finished dragging.
        /// </summary>
        public void Throw()
        {
            Vector3 force = m_BallRoot.rotation * m_Force;

            BallType ballType = m_CurrentBall.Type;
            m_CurrentBall.Throw(force);
            m_CurrentBall = null;

            AudioManager.Instance.PlayOneShot(AudioType.THROW);

            GameManager.Instance.ReduceBallCount(ballType);
        }
    }
}
