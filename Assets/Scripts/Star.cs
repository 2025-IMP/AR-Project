/// Owner: Minseong Kim
/// Description: Defines the logic when the star has been collided.

using UnityEngine;

namespace IMP.Core
{
    public class Star : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Ball")){
                Destroy(this.gameObject);
            }
        }
        void OnDestroy()
        {
            GameManager.Instance.starDestroyEvent?.Invoke();
        }
    }
}
