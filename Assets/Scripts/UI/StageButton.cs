using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IMP.UI
{
    public class StageButton : MonoBehaviour
    {
        [SerializeField]
        private string m_StageSceneName;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SceneManager.LoadScene(m_StageSceneName);
            Debug.Log("" + m_StageSceneName);
        }
    }
}
