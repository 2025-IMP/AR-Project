using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace IMP.Core
{
    public class ARPlaneHandler : MonoBehaviour
    {
        [SerializeField]
        private ARSession m_ARSession;
        
        void OnDisable()
        {
            m_ARSession.Reset();
        }
    }
}
