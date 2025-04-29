using UnityEngine;
using UnityEngine.UI;

namespace IMP.UI
{
    public class UIStar : MonoBehaviour
    {
        [SerializeField] private Image m_OffImage;
        [SerializeField] private Image m_OnImage;

        public void SetActive(bool isActive)
        {
            m_OffImage.gameObject.SetActive(!isActive);
            m_OnImage.gameObject.SetActive(isActive);
        }
    }
}

