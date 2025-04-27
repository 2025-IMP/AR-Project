// StrongBlock.cs
using UnityEngine;

namespace IMP.Core
{
    public class StrongBlock : Block
    {
        private int m_HitCount = 0;

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                m_HitCount++;
                if (m_HitCount >= 3)
                {
                    base.Initialize(); // 3번 맞아야 중력 적용
                }
            }
        }
    }
}
