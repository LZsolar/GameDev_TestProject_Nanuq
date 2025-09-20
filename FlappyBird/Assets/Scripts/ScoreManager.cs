using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    [SerializeField] List<TextMeshProUGUI> highScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI EndingScore;

    int _score=0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        SetupHighScoreText(PlayerPrefs.GetInt("HighScore"));
    }

    void SetupHighScoreText(int newHighscore)
    {
        foreach (var text in highScoreText)
        {
            text.text = newHighscore.ToString();
        }
    }
    void UpdateHighScore()
    {
        if(_score >= PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            SetupHighScoreText(_score);
        }
    }

    public void resetScore()
    {
        UpdateHighScore();
        EndingScore.text = _score.ToString();
        currentScoreText.text = "0";
        _score = 0;
    }
    public void addScore()
    {
        _score += 1;
        currentScoreText.text = _score.ToString();
    }
}
