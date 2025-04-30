/// Owner: Dongjin Kuk
/// Description: It controls the Intro Panel in Intro scene.

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.UI
{
    public class UIIntro : MonoBehaviour
    {
        [SerializeField] private GameObject m_SettingsPanel;

        public void OnStartButtonPressed()
        {
            SceneManager.LoadSceneAsync("StageScene");
        }

        public void OnSettingsButtonPressed()
        {
            m_SettingsPanel.SetActive(true);
        }

        public void OnExitButtonPressed()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
