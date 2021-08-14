using UnityEngine;
using System.Collections;
using System;
/*using GooglePlayGames;
using GooglePlayGames.BasicApi;
*/
public class GameManager : MonoBehaviour
{
    public enum GameStateEnum
    {
        menu_start, menu_score, menu_bestscore, pause, playing
    }

    static GameManager _instance;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private int missileCount = 3;

    [SerializeField]
    private GameStateEnum gameState = GameStateEnum.menu_start;

    [SerializeField]
    private int startMissileCount = 3;

    [SerializeField]
    private Boolean googlePlayConnected = false;


    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

    static public GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                {
                    GameObject go = new GameObject("_gamemanager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public delegate void GameStateChanged(GameStateEnum gameState);
    public GameStateChanged gameStateChanged;
    public delegate void MissileCountChanged(int missileCount);
    public MissileCountChanged missileCountChanged;
    public delegate void GooglePlayConnectedChanged(bool isConnected);
    public GooglePlayConnectedChanged googlePlayConnectedChanged;

    public delegate void StepBackDelegate(float stepZCount);
    public StepBackDelegate stepBack;
    

    public GameStateEnum GameState
    {
        get
        {
            return gameState;
        }

        set
        {
            gameState = value;
            if (gameStateChanged != null)
            {
                gameStateChanged(gameState);
            }
        }
    }

    public int MissileCount
    {
        get
        {
            return missileCount;
        }

        set
        {
            missileCount = value;
            if (missileCountChanged != null)
            {
                missileCountChanged(missileCount);
            }
        }
    }



    public bool GooglePlayConnected
    {
        get
        {
            return googlePlayConnected;
        }

        set
        {
            googlePlayConnected = value;
            if (googlePlayConnectedChanged != null)
            {
                googlePlayConnectedChanged(googlePlayConnected);
            }
        }
    }

    internal ScoreManager ScoreManager
    {
        get
        {
            return scoreManager;
        }

        set
        {
            scoreManager = value;
        }
    }

    public LevelManager LevelManager
    {
        get
        {
            return levelManager;
        }

        set
        {
            levelManager = value;
        }
    }

    public void Start()
    {
        if (levelManager == null)
        {
            levelManager = GetComponent<LevelManager>();
        }
        if (levelManager == null)
        {
            throw new MissingComponentException("LevelManager");
        }
        ScoreManager.BestScore = PlayerPrefs.GetInt("BestScore");
        initGooglePlay();
    }

    private void initGooglePlay()
    {
       /* Social.localUser.Authenticate((bool success) => {
            GooglePlayConnected = success;
            Debug.Log("Google Play connection status : " + success);
            if (success)
            {
                // Load best score from google.
                PlayGamesPlatform.Instance.LoadScores(
                    PlayServiceConstants.leaderboard_high_score,
                    LeaderboardStart.PlayerCentered,
                    10,
                    LeaderboardCollection.Public,
                    LeaderboardTimeSpan.AllTime,
                    (data) =>
                    {
                        if (data.Valid)
                        {
                            scoreManager.BestScore = data.PlayerScore.value;
                        }
                    }
                );
            }
        });*/
    }
    
    public void StepBack(float stepBackZ)
    {
      //  distance += stepBackZ;
        if (stepBack != null)
        {
            stepBack(stepBackZ);
        }
    }

    public void StartGame()
    {
        ScoreManager.StartGame();
        MissileCount = startMissileCount;
        GameState = GameStateEnum.playing;
    }

    public void PlayerDied()
    {
        if (gameState == GameStateEnum.playing)
        {
            if (ScoreManager.Score >= ScoreManager.BestScore)
            {
                GameState = GameStateEnum.menu_bestscore;
            }
            else
            {
                GameState = GameStateEnum.menu_score;
            }
            ScoreManager.PlayerDied();
        }
    }

}
