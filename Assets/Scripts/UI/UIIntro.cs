/// Owner: Dongjin Kuk
/// Description: It controls the Intro Panel in Intro scene.

using IMP.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.UI
{
    public class UIIntro : MonoBehaviour
    {
        [SerializeField] private GameObject m_SettingsPanel;

        void Start()
        {
            PlayerPrefs.DeleteAll();
            AudioManager.Instance.PlayBGM(AudioManager.Instance.IntroBGM);
        }

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
