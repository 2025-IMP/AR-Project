/// Owner: Minseong Kim
/// Description: Defines the logic when the star has been collided.

using IMP.Common;
using IMP.UI;
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
                AudioManager.Instance.PlayOneShot(AudioType.STAR);
                Destroy(gameObject);
                GameManager.Instance.CollectStar();
            }
        }
    }
}
