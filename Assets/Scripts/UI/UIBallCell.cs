using IMP.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IMP.UI
{
    public class UIBallCell : MonoBehaviour
    {
        private BallType m_BallType = BallType.NORMAL;
        public BallType BallType => m_BallType;

        [SerializeField] private TMP_Text m_CountText;
        public TMP_Text CountText => m_CountText;
        [SerializeField] private Image m_SelectedImage;
        [SerializeField] private Image m_BallImage;

        [SerializeField]
        private Button m_Button;
        public Button Button => m_Button;

        private bool m_Selected = false;
        public bool Selected => m_Selected;

        public void Config(BallType type, int count)
        {
            m_BallType = type;
            m_CountText.text = count.ToString();
            m_BallImage.sprite = BallManager.GetBallSprite(type);
            m_Selected = false;
        }

        public void SetCount(int count)
        {
            m_CountText.text = count.ToString();
        }

        public void Select()
        {
            m_Selected = true;
            m_SelectedImage.gameObject.SetActive(true);

            GameManager.Instance.SpawnBall(m_BallType);
        }

        public void Deselect()
        {
            m_Selected = false;
            m_SelectedImage.gameObject.SetActive(false);
        }
    }
}
