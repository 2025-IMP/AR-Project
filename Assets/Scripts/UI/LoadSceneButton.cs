using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public static int stageNumber;
        public void OnButtonPressed(String sceneName)
        {
            if(sceneName.StartsWith("Stage")&& sceneName.Length == 6){
                stageNumber = int.Parse(sceneName.Substring(5));
            }
            SceneManager.LoadSceneAsync("Game");
        }
    }
}
