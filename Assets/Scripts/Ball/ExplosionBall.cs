// ExplosionBall.cs
using UnityEngine;

namespace IMP.Core
{
    public class ExplosionBall : Ball
    {
        [SerializeField] private float m_ExplosionRadius = 3f;

        [SerializeField] private float m_ExplosionForce = 500f;

        protected override void OnCollisionEnter(Collision collision)
        {
            if (m_Ghosted)
            {
                return;
            }

            // 충돌한 위치 중심으로 폭발
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, m_ExplosionRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(m_ExplosionForce, explosionPos, m_ExplosionRadius);
                }
            }

            Destroy(gameObject);
        }
    }
}
