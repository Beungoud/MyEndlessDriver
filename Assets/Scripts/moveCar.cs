using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class moveCar : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 1;

    [SerializeField]
    private float maxSpeed = 2;

    [SerializeField]
    private int currentLane = 1;

    [SerializeField]
    private int moveSmoothFactor = 2;

    private StreetDefinition streetDefinition;
    private float X;
    private float Y;

    private float targetX;
    private float startX;
    private float speedRatio;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float breaking = 3f;
    [SerializeField]
    private float acceleration = 1.5f;
    //[SerializeField]
    //private AudioSource engineAudioSource;

    private float startDistance = 0;
    private bool isGameRunning = false;

    // The car is driving in direction of Z axis.

    void Start()
    {
        GameManager.instance.stepBack += StepBack;

        streetDefinition = GetComponentInParent<StreetDefinition>();
        X = transform.position.x - currentLane * streetDefinition.LaneSeparation;
        Y = transform.position.y;

        GameManager.instance.gameStateChanged += GameStateChanged;
        GameManager.instance.ScoreManager.distanceChanged += DistanceChanged;
             
        //if (engineAudioSource == null)
        //{
        //    engineAudioSource = GetComponentInChildren<AudioSource>();
        //}
    }

    private void DistanceChanged(float distance)
    {
        currentSpeed = minSpeed + (maxSpeed - minSpeed) * Mathf.Sqrt(distance / 60000f);
    }

    void GameStateChanged(GameManager.GameStateEnum status)
    {
        Debug.Log("Move Vehicle, gameStateChanged " + status);
        if (status == GameManager.GameStateEnum.playing)
        {
            isGameRunning = true;
            startDistance = transform.position.z;
        } else
        {
            isGameRunning = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            if (UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown("Left") || Input.GetButtonDown("Left"))
            {
                currentLane--;
            }
            if (UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown("Right") || Input.GetButtonDown("Right"))
            {
                currentLane++;
            }
            if (currentLane >= streetDefinition.NbLanes)
            {
                currentLane = streetDefinition.NbLanes - 1;
                transform.position = transform.position + new Vector3(streetDefinition.LaneSeparation / 10, 0, 0);
            }
            if (currentLane < 0)
            {
                currentLane = 0;
                transform.position = transform.position - new Vector3(streetDefinition.LaneSeparation / 10, 0, 0);
            }
            targetX = X + currentLane * streetDefinition.LaneSeparation;


            if (CrossPlatformInputManager.GetAxis("Vertical") < 0 || Input.GetButton("Down"))
            {
                speedRatio -= breaking * Time.deltaTime;
            }

            speedRatio += acceleration * Time.deltaTime;
            if (speedRatio > 100)
            {
                speedRatio = 100;
            }
            if (speedRatio < 0)
            {
                speedRatio = 0;
            }
        }
     //   engineAudioSource.pitch = 0.7f + 2f * speedRatio / 100 ;
     //   currentSpeed = minSpeed + (maxSpeed - minSpeed) * speedRatio / 100;
    }

    public void FixedUpdate()
    {
        //transform.position = transform.position + new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
        transform.position = new Vector3((targetX + moveSmoothFactor * transform.position.x) / (moveSmoothFactor+1), 
            Y, 
            transform.position.z + currentSpeed);

        // Update current distance
        GameManager.instance.ScoreManager.Distance = transform.position.z - startDistance;
    }

    public void StepBack(float distance)
    {
        startDistance -= distance;
    }
    
}
