using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace IMP.Core
{
    public class Block : MonoBehaviour
    {
        protected Rigidbody m_Rigidbody;

        public virtual void Initialize()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Rigidbody.useGravity = false;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                m_Rigidbody.useGravity = true;
            }
        }
    }
}
