/// Owner: Dongjin Kuk
/// Description: It controls the Intro Panel in Intro scene.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.UI
{
    public class UIIntro : MonoBehaviour
    {
        public void OnStartButtonPressed()
        {
            SceneManager.LoadSceneAsync("StageScene");
        }
    }
}
