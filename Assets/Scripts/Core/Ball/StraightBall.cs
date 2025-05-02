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
            m_Rigidbody.useGravity = false;
        }

        public override void Simulate(Vector3 force)
        {
            m_Ghosted = true;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);

            gameObject.layer = LayerMask.NameToLayer("Ball");
        }

        public override void Throw(Vector3 force)
        {
            m_Ghosted = false;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);

            gameObject.layer = LayerMask.NameToLayer("Ball");

            StartCoroutine(DestroyCoroutine());
        }
    }
}
