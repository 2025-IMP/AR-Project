using System;
using IMP.Core;
using TMPro;
using UnityEngine;

public class GameUImanager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject stageClearPanel;
    public TMP_Text ballCountText;
    public TMP_Text starCountText;

    public Action<int> OnBallCountChanged;

    void OnEnable()
    {
        OnBallCountChanged += SetBallCount;
    }

    void OnDisable()
    {
        OnBallCountChanged -= SetBallCount;
    }

    public void SetBallCount(int ballCount)
    {
        ballCountText.text = $"Ball {ballCount}/{GameManager.Instance.StageData.BallPrefabs.Count}";
    }

    public void SetStarCount(int starCount)
    {

    }
}
