/// Owner: Minseong Kim
/// Description: It gives callback function to the stage button.

using System;
using System.Collections.Generic;
using System.Linq;
using IMP.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IMP.UI
{
    public class UIStagePanel : MonoBehaviour
    {
        [SerializeField] private Sprite m_DefaultTexture;
        [SerializeField] private Sprite m_ClearTexture;

        [SerializeField]
        private Transform m_StageRoot;
        private List<Image> m_StageImages = new List<Image>();
        private readonly string[] STAGE_NAMES = {"stage1", "stage2", "stage3"};

        void Awake()
        {
            m_StageImages = m_StageRoot.GetComponentsInChildren<Image>().ToList();
            foreach (var img in m_StageImages)
            {
                Debug.Log(img.gameObject.name);
            }
        }

        void OnEnable()
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(PlayerPrefs.GetInt(STAGE_NAMES[i]));
                if (PlayerPrefs.GetInt(STAGE_NAMES[i]) == 1)
                {
                    m_StageImages[i].sprite = m_ClearTexture;
                }
                else
                {
                    m_StageImages[i].sprite = m_DefaultTexture;
                }
            }
        }

        public void OnButtonPressed(int stageIndex)
        {
            StageManager.Instance.SetStageData(stageIndex);
            SceneManager.LoadScene("Game");
        }

        public void OnBackButtonPressed()
        {
            SceneManager.LoadScene("Intro");
        }
    }
}
