/// Owner: Donghun Lee
/// Description: Special type of the block.

using UnityEngine;

namespace IMP.Core
{
    public class StrongBlock : Block
    {
        private Renderer m_Renderer;
        private float m_MaxR = 255f;
        private float m_MinR = 39f;

        private int m_HitCount = 0;

        void Awake()
        {
            m_Renderer = GetComponentInChildren<Renderer>();
        }

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
                else
                {
                    float ratio = 1f - (m_HitCount / 2f);

                    Color rcolor = m_Renderer.material.color;
                    rcolor.r = ratio;
                    m_Renderer.material.color = rcolor;
                }
            }
        }
    }
}
