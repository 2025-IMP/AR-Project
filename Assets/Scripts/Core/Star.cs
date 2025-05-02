/// Owner: Minseong Kim
/// Star in Structure; Defines the star's life cycle.

using IMP.Core;
using UnityEngine;

public class Star : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {
        GameManager.Instance.CollectStar();
    }
}
