using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class LevelManager : MonoBehaviour
{
    [Serializable]
    public class LevelDefinition
    {
        [SerializeField]
        public GameObject[] decoList;
        [SerializeField]
        public String name;
    }
    [SerializeField]
    private int levelNumber;

    [SerializeField]
    private LevelDefinition[] levelDefinitions;

    [SerializeField]
    private int levelLenght = 3000;

    public delegate void LevelChanged(int level);
    public LevelChanged levelChanged;
        
    public int LevelNumber
    {
        get
        {
            return levelNumber;
        }

        set
        {
            if (levelNumber != value)
            {
                levelNumber = value;
                if (levelChanged != null)
                {
                    levelChanged(levelNumber);
                }
            }
        }
    }

    public void GameStart()
    {
        LevelNumber = 0;
        if (levelChanged != null)
        {
            levelChanged(levelNumber);
        }
    }

    public LevelDefinition CurrentLevelDefinition
    {
        get
        {
            return levelDefinitions[LevelNumber % levelDefinitions.Length];
        }
    }

    public void Start()
    {
        GameManager.instance.ScoreManager.distanceChanged += DistanceChanged;
        GameManager.instance.gameStateChanged += GameStateChanged;

    }

    private void GameStateChanged(GameManager.GameStateEnum gameState)
    {
        if (gameState == GameManager.GameStateEnum.playing)
        {
            GameStart();
        }
    }

    private void DistanceChanged(float distance)
    {
        LevelNumber = ((int)distance / levelLenght);
    }
}
