using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TapToPlay;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI highScore;

    private int score = 0;

    private void Awake()
    {
        int score = PlayerPrefs.GetInt("LastScore");
        textScore.text = score.ToString();

        int highScore = PlayerPrefs.GetInt("HighScore");
        this.highScore.text = ($"±â·Ï : {highScore}");
    }

    public void GameStart()
    {
        TapToPlay.enabled = false;
        highScore.enabled = false;
        textScore.text = score.ToString();
    }

    public void IncreasingScore()
    {
        score++;
        textScore.text = score.ToString();
    }

    public void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        PlayerPrefs.SetInt("LastScore", score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LastScore", 0);
    }
}
