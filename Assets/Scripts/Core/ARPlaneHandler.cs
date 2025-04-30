/// Owner: Dongjin Kuk
/// Description: There's an issue when the AR Session is being disabled.
/// So we have to reset it additionally.

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
