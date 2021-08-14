using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
class ScoreManager
{

    [SerializeField]
    private float bestScore = 0;

    [SerializeField]
    private int prevScore;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float previousDistance = 0;

    [SerializeField]
    private int coinCount = 0;

    [SerializeField]
    private int coinScoreMultiplier = 50;

    public delegate void CoinCountChanged(int coinCount);
    public CoinCountChanged coinCountChanged;
    public delegate void DistanceChanged(float distance);
    public DistanceChanged distanceChanged;

    public float BestScore
    {
        get
        {
            return bestScore;
        }
        set
        {
            bestScore = value;
            PlayerPrefs.SetInt("BestScore", (int)bestScore);
        }
    }

    public int Score
    {
        get
        {
            return (int)distance + coinScoreMultiplier * coinCount;
        }
    }

    public int CoinCount
    {
        get
        {
            return coinCount;
        }

        set
        {
            coinCount = value;
            if (coinCountChanged != null)
            {
                coinCountChanged(coinCount);
            }
        }
    }

    public int PrevScore
    {
        get
        {
            return prevScore;
        }

        set
        {
            prevScore = value;
        }
    }

    public float Distance
    {
        get
        {
            return distance;
        }

        set
        {
            if (GameManager.instance.GameState == GameManager.GameStateEnum.playing)
            {
                distance = value;
                if (distanceChanged != null)
                {
                    distanceChanged(distance);
                }
            }
        }
    }

    public float PreviousDistance
    {
        get
        {
            return previousDistance;
        }

        private set
        {
            previousDistance = value;
        }
    }

    internal void StartGame()
    {
        CoinCount = 0;
        distance = 0;
    }

    internal void PlayerDied()
    {
        previousDistance = distance;
        PrevScore = Score;
        postScoreOnGoolge((int)Score);
        manageAchievements((int)Score);
        if (Score >= BestScore)
        {
            BestScore = Score;
        }
    }


    private void manageAchievements(int score)
    {
        if (GameManager.instance.GooglePlayConnected)
        {
            Action<bool> action = (bool success) =>
            {
                Debug.Log("Achievement status : " + success);
            };
            Social.ReportProgress(PlayServiceConstants.achievement_first_run, 100.0f, action);
            Social.ReportProgress(PlayServiceConstants.achievement_thousand, 0f, action);
            if (score > 1000)
            {
                Social.ReportProgress(PlayServiceConstants.achievement_thousand, 100.0f, action);
                Social.ReportProgress(PlayServiceConstants.achievement_3000, 0f, action);
            }
            if (score > 3000)
            {
                Social.ReportProgress(PlayServiceConstants.achievement_3000, 100.0f, action);
                Social.ReportProgress(PlayServiceConstants.achievement_6000, 0f, action);
            }
            if (score > 6000)
            {
                Social.ReportProgress(PlayServiceConstants.achievement_6000, 100.0f, action);
                Social.ReportProgress(PlayServiceConstants.achievement_10000, 0f, action);
            }
            if (score > 10000)
            {
                Social.ReportProgress(PlayServiceConstants.achievement_10000, 100.0f, action);
                Social.ReportProgress(PlayServiceConstants.achievement_20000, 0f, action);
            }
            if (score > 20000)
            {
                Social.ReportProgress(PlayServiceConstants.achievement_20000, 100.0f, action);
                Social.ReportProgress(PlayServiceConstants.achievement_50000, 0f, action);
            }
            if (score > 50000)
            {
                Social.ReportProgress(PlayServiceConstants.achievement_50000, 100.0f, action);
                Social.ReportProgress(PlayServiceConstants.achievement_100000, 0f, action);
            }
            if (score > 100000)
            {
                Social.ReportProgress(PlayServiceConstants.achievement_100000, 100.0f, action);
                //     Social.ReportProgress(PlayServiceConstants.achievement_100000, 0f, action);
            }
        }
    }

    private void postScoreOnGoolge(int score)
    {
        if (GameManager.instance.GooglePlayConnected)
        {
            Social.ReportScore(score, PlayServiceConstants.leaderboard_high_score, (bool success) =>
            {
                Debug.Log("Top score upload status : " + success);
            });
        }
    }
}
