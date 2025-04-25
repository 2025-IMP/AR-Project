using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace IMP.Core
{
    public class Slingshot : MonoBehaviour
    {
        [SerializeField]
        private TrajectoryPrediction m_TrajPrediction;

        [SerializeField] private Transform m_BallRoot;
        [SerializeField] private Ball m_BallPrefab;

        [SerializeField] private float m_HorzDeltaBoundary = 300f;
        [SerializeField] private float m_VertDeltaBoundary = 500f;

        [SerializeField] private float m_HorzPowerBoundary = 30f;
        [SerializeField] private float m_VertPowerBoundary = 30f;

        private Vector2 m_TouchPos;
        private Vector2 m_DeltaPos;
        private Vector3 m_Force;

        private bool m_Touching = false;

        private void Update()
        {
            HandleInput();

            if (m_Touching)
            {
                Vector3 force = Quaternion.Inverse(m_BallRoot.rotation) * m_Force;
                m_TrajPrediction.Simulate(m_BallPrefab, m_BallRoot.position, force);
            }
            else
            {
                m_TrajPrediction.StopSimulation();
            }
        }

        private void HandleInput()
        {
            var touches = Touch.activeTouches;

            if (touches.Count == 1 && touches[0].phase == TouchPhase.Began)
            {
                m_TouchPos = touches[0].screenPosition;
                m_Touching = true;
            } else if (touches.Count == 1 && touches[0].phase == TouchPhase.Stationary)
            {
                m_DeltaPos = m_TouchPos - touches[0].screenPosition;
                CalculateForce();
            } else if (touches.Count == 1 && touches[0].phase == TouchPhase.Ended && GameManager.Instance.State == GameManager.GameState.BUILT)
            {
                m_DeltaPos = m_TouchPos - touches[0].screenPosition;
                CalculateForce();
                Throw();

                m_Touching = false;
            }
        }

        private void CalculateForce()
        {
            float horzRatio = Mathf.Abs(m_DeltaPos.x) / m_HorzDeltaBoundary;
            horzRatio = Mathf.Min(horzRatio, 1f);
            float horzPower = Mathf.Lerp(0f, m_HorzPowerBoundary, horzRatio) * Mathf.Sign(m_DeltaPos.x);

            float vertRatio = Mathf.Abs(m_DeltaPos.y) / m_VertDeltaBoundary;
            vertRatio = Mathf.Min(vertRatio, 1f);
            float vertPower = Mathf.Lerp(0f, m_VertPowerBoundary, vertRatio) * Mathf.Sign(m_DeltaPos.y);

            m_Force = new Vector3(horzPower, 1f, vertPower);
        }

        public void Throw()
        {
            Ball ball = Instantiate(m_BallPrefab);

            Vector3 force = Quaternion.Inverse(m_BallRoot.rotation) * m_Force;
            ball.Throw(m_BallRoot.position, force);
        }
    }
}
