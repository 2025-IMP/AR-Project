/// Owner: Minseong Kim
/// Description: Defines the logic when the star has been collided.

using UnityEngine;

namespace IMP.Core
{
    public class Star : MonoBehaviour
    {
        public void Initialize()
        {
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Ball")){
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
        }
    }
}
