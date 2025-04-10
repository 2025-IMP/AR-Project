using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace IMP.Core
{
    public class ThrowController : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_BallPrefab;

        private Vector2 m_TouchPos;
        private Vector2 m_ReleasePos;
        private bool m_Touched = false;

        private void OnEnable()
        {
            TouchSimulation.Enable();
            EnhancedTouchSupport.Enable();
        }

        private void Update()
        {
            var mouse = Mouse.current;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Touched!");
            }

            if (Input.GetMouseButtonDown(0) && !m_Touched)
            {
                m_TouchPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                m_Touched = true;
            }
            if (Input.GetMouseButtonUp(0) && m_Touched)
            {
                m_Touched = false;
                m_ReleasePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Throw();
            }
        }

        private void Throw()
        {
            Vector2 diff = m_ReleasePos - m_TouchPos;
            Vector3 power = new Vector3(diff.x, diff.y, diff.y);

            GameObject ballObj = Instantiate(m_BallPrefab);
            ballObj.transform.position = m_ReleasePos;

            Debug.Log($"power: ({power.x}, {power.y}, {power.z})");

            Rigidbody ballRb = ballObj.GetComponent<Rigidbody>();
            ballRb.AddForce(power);
        }
    }
}
