using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private void Start()
    {
        GameManager.Instance.onScoring += ScoreZone_onScoring;
    }
    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.onScoring -= ScoreZone_onScoring;
        }
    }
    private void ScoreZone_onScoring(object sender, System.EventArgs e)
    {
        scoreText.text = "SCORE: " + GameManager.Instance.gameScore;
    }
}
