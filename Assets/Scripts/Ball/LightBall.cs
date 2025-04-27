using UnityEngine;

namespace IMP.Core
{
    public class LightBall : Ball
    {
        protected override void Awake()
        {
            base.Awake();
            // 더 가볍게 만듦
            m_Rigidbody.mass = 0.5f;
        }

        public override void Simulate(Vector3 force)
        {
            // 더 빠르게 날라감감
            base.Simulate(force * 1.2f);
        }
    }
}
