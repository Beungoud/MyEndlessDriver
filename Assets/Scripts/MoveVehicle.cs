using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class MoveVehicle : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 0.3f;

    [SerializeField]
    private float maxSpeed = 0.45f;

    [SerializeField]
    private int currentLane = 1;

    private StreetDefinition streetDefinition;

    private float startX;
    private float speedRatio;

    [SerializeField]
    public float currentSpeed;

    public Transform aliveLimit;

    public int CurrentLane
    {
        get
        {
            return currentLane;
        }

        set
        {
            currentLane = value;
        }
    }

    // The car is driving in direction of Z axis.

    void Start()
    {
        streetDefinition = GetComponentInParent<StreetDefinition>();
        speedRatio = Random.Range(0, 100);
        currentSpeed = minSpeed + (maxSpeed - minSpeed) * speedRatio / 100;

        // Realign the vehicle on the street
        float targetX = streetDefinition.StreetX + currentLane * streetDefinition.LaneSeparation;
        transform.position = new Vector3(targetX,
            streetDefinition.StreetY,
            transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        if (aliveLimit != null)
        {
            if (transform.position.z < aliveLimit.transform.position.z)
            {
                Destroy(gameObject);
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.forward, out hit, 12f, LayerMask.GetMask("Vehicle"))) 
        {
            MoveVehicle otherVehicle = hit.collider.GetComponent<MoveVehicle>();
            currentSpeed = otherVehicle.currentSpeed;                
            Debug.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + Vector3.forward * 12, Color.red);
        } else
        {
            Debug.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + Vector3.forward * 12, Color.blue);
        }

    }

    public void FixedUpdate()
    {
        float targetX = streetDefinition.StreetX + currentLane * streetDefinition.LaneSeparation;

        transform.position = new Vector3(targetX,
            streetDefinition.StreetY,
            transform.position.z + currentSpeed);

    }

    public void OnDrawGizmos()
    {
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 12);
    }
}
