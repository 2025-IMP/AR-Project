/// Owner: Dongjin Kuk
/// Description: This script is for ball. It throws and simulate physics itself.

using System.Collections;
using UnityEngine;

namespace IMP.Core
{
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        protected Rigidbody m_Rigidbody;

        [SerializeField]
        private float m_LifeTime = 5f;

        protected virtual void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        protected bool m_Ghosted = false;

        public virtual void Simulate(Vector3 force)
        {
            m_Ghosted = true;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public virtual void Throw(Vector3 pos, Vector3 force)
        {
            m_Ghosted = false;
            transform.position = pos;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);

            StartCoroutine(DestroyCoroutine());
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (m_Ghosted)
                return;
        }

        protected virtual IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(m_LifeTime);

            Destroy(gameObject);
        }
    }
}
