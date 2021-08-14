using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class InstantiateVehicle : MonoBehaviour
{
    public StreetDefinition street;
    public MoveVehicle[] decoList ;

    public float lastGenerated = 0;
    [SerializeField] private float vehicleSteps = 10;

    [SerializeField] private int probability = 30;

    [SerializeField]
    private Transform aliveLimit;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.stepBack += stepBack;
        // Load all available resources
        decoList = Resources.LoadAll<MoveVehicle>("Vehicle");
        Debug.Log("Number of vehicle loaded : " + decoList.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > lastGenerated + vehicleSteps)
        {
            lastGenerated = transform.position.z;
            int rnd = UnityEngine.Random.Range(0, 100);
            if (rnd < probability)
            {
                spawnVehicle();
            }
        }
    }

    private void spawnVehicle()
    {
        if (GameManager.instance.GameState == GameManager.GameStateEnum.playing)
        {
            int value = UnityEngine.Random.Range(0, decoList.Length);

            MoveVehicle vehicle = Instantiate<MoveVehicle>(decoList[value]);
            vehicle.transform.position = transform.position;
            vehicle.aliveLimit = aliveLimit;
            vehicle.CurrentLane = UnityEngine.Random.Range(0, street.NbLanes);
            vehicle.transform.parent = street.transform;
        }
    }

    void stepBack(float distance)
    {
        lastGenerated -= distance;
    }

}
