using UnityEngine;

namespace IMP.Core
{
    public class Block : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;

        public void Initialize()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Rigidbody.useGravity = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                m_Rigidbody.useGravity = true;
            }
        }
    }
}
