using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int currentScore;
    private int highScore;

    void Start()
    {
        currentScore = 0;
        highScore = 0;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
