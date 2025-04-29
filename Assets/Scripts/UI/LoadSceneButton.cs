/// Owner: Minseong Kim
/// Description: It gives callback function to the stage button.

using IMP.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public void OnButtonPressed(int stageIndex)
        {
            StageManager.Instance.SetStageData(stageIndex);
            SceneManager.LoadScene("Game");
        }
    }
}
