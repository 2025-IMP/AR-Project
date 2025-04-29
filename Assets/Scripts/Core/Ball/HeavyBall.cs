/// Owner: Donghun Lee
/// Description: Special type of the ball.

using UnityEngine;

namespace IMP.Core
{
    public class HeavyBall : Ball
    {
        protected override void Awake()
        {
            base.Awake();
            m_Rigidbody.mass = 5f;
        }

        public override void Simulate(Vector3 force)
        {
            base.Simulate(force * 0.8f); // 덜 멀리 날아가게
        }
    }
}
