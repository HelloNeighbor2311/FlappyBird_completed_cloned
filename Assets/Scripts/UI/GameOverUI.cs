using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private const string HIGH_SCORE = "HighScore";

    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] Button retryBtn;
    [SerializeField] Button mainMenuBtn;

    void Start()
    {
        mainMenuBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.SceneEnum.MainMenu);
        });
        retryBtn.onClick.AddListener(() =>
        {
            RetryBtn();
        });
        Player.Instance.onGameOver += Player_onGameOver;
        Hide();
    }

    private void Player_onGameOver(object sender, System.EventArgs e)
    {
        currentScore.text = "SCORE: " + GameManager.Instance.gameScore;
        HandleHighScore();
        Show();
    }
    private void Show()
    {
        this.gameObject.SetActive(true);
    }
    private void Hide() { 
        this.gameObject.SetActive(false);
    }
    private void RetryBtn()
    {
        SceneLoader.LoadScene(SceneLoader.SceneEnum.GamePlayScene);
    }
    private void HandleHighScore()
    {
        int score = GameManager.Instance.gameScore;
        if (PlayerPrefs.HasKey(HIGH_SCORE))
        {
            if (score > PlayerPrefs.GetInt(HIGH_SCORE))
            {
                PlayerPrefs.SetInt(HIGH_SCORE, score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(HIGH_SCORE, score);
        }
        highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt(HIGH_SCORE);
    }
}
