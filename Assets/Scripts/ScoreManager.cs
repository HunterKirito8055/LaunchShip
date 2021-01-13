using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    string scoreKey = "scoreKey";
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            DisplayScore(score);
        }
    }
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        PlayerPrefs.GetInt(scoreKey, 0);
    }
    private void Update()
    {
      
    }
    public void AddScore(int _scoreValue)
    {
        Score += _scoreValue;
        PlayerPrefs.SetInt(scoreKey, Score);
    }
    public void ResetScore()
    {
        Score = 0;
        DisplayScore(Score);
    }
    private void DisplayScore(int _score)
    {
        GameManager.sharedInstance.DisplayScore(_score);
    }
}
