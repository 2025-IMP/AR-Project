using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.UI
{
	public class UIIntro : MonoBehaviour
	{
        public void OnStartButtonPressed()
        {
            SceneManager.LoadSceneAsync("Game");
        }
	}
}
