using System;
using IMP.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public static int stageNumber;
        
        public void OnButtonPressed(int stageIndex)
        {
            StageManager.Instance.SetStageData(stageIndex);
            SceneManager.LoadScene("Game");
        }
    }
}
