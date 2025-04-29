/// Owner: Donghun Lee
/// Description: Special type of the block.

using UnityEngine;

namespace IMP.Core
{
    public class StrongBlock : Block
    {
        private int m_HitCount = 0;

        public override void Initialize()
        {
            base.Initialize();
            m_Rigidbody.isKinematic = true;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                m_HitCount++;
                if (m_HitCount >= 3)
                {
                    m_Rigidbody.isKinematic = false;
                    m_Rigidbody.useGravity = true;
                }
            }
        }
    }
}
