/// Owner: Donghun Lee
/// Description: Special type of the ball.

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

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, m_ExplosionRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].transform.parent.CompareTag("Block")) continue;

                if (colliders[i].transform.parent.TryGetComponent(out Block block))
                {
                    block.Rigidbody.isKinematic = false;
                    block.Rigidbody.useGravity = true;
                }

                if (colliders[i].transform.parent.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(m_ExplosionForce, explosionPos, m_ExplosionRadius);
                }
            }

            Destroy(gameObject);
        }
    }
}
