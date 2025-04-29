/// Owner: Donghun Lee
/// Description: Special type of the ball.

using UnityEngine;

namespace IMP.Core
{
    public class StraightBall : Ball
    {
        private Vector3 m_Direction;

        [SerializeField]
        private float m_Speed = 10f;

        protected override void Awake()
        {
            base.Awake();
            m_Rigidbody.useGravity = false; // 중력 아예 꺼버려
        }

        public override void Throw(Vector3 pos, Vector3 force)
        {
            m_Ghosted = false;
            transform.position = pos;
            m_Direction = force.normalized; // 방향만 저장

            StartCoroutine(DestroyCoroutine());
        }

        private void Update()
        {
            if (!m_Ghosted)
            {
                transform.position += m_Direction * m_Speed * Time.deltaTime;
            }
        }
    }
}
