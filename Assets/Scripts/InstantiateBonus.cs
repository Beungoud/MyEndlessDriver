using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class InstantiateBonus: MonoBehaviour
{
    [System.Serializable]
    public class bonusDef
    {
        public GameObject bonusObject;

        public int probability ;
        
        public int startProba;
    }
    public StreetDefinition street;

    [SerializeField]
    Transform destructionLimit;

    [SerializeField]
    public bonusDef[] bonusList ;
    private int totalProba;

    public float lastGenerated = 0;
    [SerializeField] private float bonusSteps = 10;

    [SerializeField] private int globalProbability = 30;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.stepBack += stepBack;

        totalProba = 0;
        foreach (bonusDef bonus in bonusList)
        {
            bonus.startProba = totalProba;
            totalProba += bonus.probability;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > lastGenerated + bonusSteps)
        {
            lastGenerated = transform.position.z;
            int rnd = UnityEngine.Random.Range(0, 100);
            if (rnd < globalProbability)
            {
                spawnBonus();
            }
        }
    }

    private void spawnBonus()
    {
        if (GameManager.instance.GameState == GameManager.GameStateEnum.playing)
        {
            int value = UnityEngine.Random.Range(0, totalProba);
            foreach (bonusDef bonus in bonusList)
            {
                if (value >= bonus.startProba && value < bonus.startProba + bonus.probability)
                {
                    GameObject instantiatedGameObject = Instantiate<GameObject>(bonus.bonusObject);
                    instantiatedGameObject.transform.position = street.getPositionForLane(UnityEngine.Random.Range(0, street.NbLanes), transform.position.z);
                    instantiatedGameObject.AddComponent<BonusDestroy>().destroyLimit = destructionLimit;
                }
            }
        }
    }

    void stepBack(float distance)
    {
        lastGenerated -= distance;
    }
}
