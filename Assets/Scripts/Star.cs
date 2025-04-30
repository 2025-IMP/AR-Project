using IMP.Common;
using IMP.Core;
using UnityEngine;

public class Star : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayOneShot(AudioType.STAR);
        if (other.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {
        GameManager.Instance.starDestroyEvent?.Invoke();
    }
}
