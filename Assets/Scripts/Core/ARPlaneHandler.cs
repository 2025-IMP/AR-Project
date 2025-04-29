using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace IMP.Core
{
    public class ARPlaneHandler : MonoBehaviour
    {
        [SerializeField]
        private ARPlaneManager m_ARPlaneManager;
        
        void OnDisable()
        {
            foreach (var plane in m_ARPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
            }
        }
    }
}
