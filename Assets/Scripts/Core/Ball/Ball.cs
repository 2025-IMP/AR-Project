/// Owner: Dongjin Kuk
/// Description: This script is for ball. It throws and simulate physics itself.

using System.Collections;
using IMP.Common;
using UnityEngine;

namespace IMP.Core
{
    public enum BallType
    {
        NORMAL,
        EXPLOSION,
        HEAVY,
        LIGHT,
        STRAIGHT
    }

    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private BallType m_Type = BallType.NORMAL;
        public BallType Type => m_Type;

        [SerializeField]
        protected Rigidbody m_Rigidbody;

        [SerializeField]
        private float m_LifeTime = 5f;

        private bool m_Collided = false;

        protected virtual void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Rigidbody.useGravity = false;
        }

        protected bool m_Ghosted = false;

        public virtual void Simulate(Vector3 force)
        {
            m_Ghosted = true;
            m_Rigidbody.useGravity = true;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public virtual void Throw(Vector3 force)
        {
            m_Ghosted = false;
            transform.SetParent(null);            

            m_Rigidbody.useGravity = true;
            m_Rigidbody.AddForce(force, ForceMode.Impulse);

            StartCoroutine(DestroyCoroutine());
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (m_Ghosted)
                return;

            if (!m_Collided)
            {
                AudioManager.Instance.PlayOneShot(AudioType.IMPACT);
                m_Collided = true;
            }
        }

        protected virtual IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(m_LifeTime);

            Destroy(gameObject);
        }
    }
}
