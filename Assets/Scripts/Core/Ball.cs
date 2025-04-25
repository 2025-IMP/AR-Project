using System.Collections;
using UnityEngine;

namespace IMP.Core
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_Rigidbody;

        [SerializeField] private float m_LifeTime = 5f;

        private bool m_Ghosted = false;

        public void Simulate(Vector3 force)
        {
            m_Ghosted = true;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public void Throw(Vector3 pos, Vector3 force)
        {
            m_Ghosted = false;
            transform.position = pos;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);

            StartCoroutine(DestroyCoroutine());
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (m_Ghosted) return;
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(m_LifeTime);

            Destroy(gameObject);
        }
    }
}
