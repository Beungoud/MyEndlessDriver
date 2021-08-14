using UnityEngine;
using System.Collections;

public class StreetDefinition : MonoBehaviour {

    [SerializeField]
    private int nbLanes = 3;

    [SerializeField]
    private float firstLanePosition = -3;

    [SerializeField]
    private float laneSeparation = 3;

    private float streetX;
    private float streetY;

    public float LaneSeparation
    {
        get
        {
            return laneSeparation;
        }       
    }

    public float FirstLanePosition
    {
        get
        {
            return firstLanePosition;
        }
    }

    public int NbLanes
    {
        get
        {
            return nbLanes;
        }
    }

    public float StreetX
    {
        get
        {
            return streetX;
        }
    }

    public float StreetY
    {
        get
        {
            return streetY;
        }
    }

    // The car is driving in direction of Z axis.

    void Start()
    {
        streetX = transform.position.x + firstLanePosition;
        streetY = transform.position.y;

    }

    public Vector3 getPositionForLane(int laneNumeber, float z)
    {
        return new Vector3(firstLanePosition + laneNumeber * laneSeparation, 0, z);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < nbLanes; i++)
        {
            Gizmos.DrawLine(new Vector3(firstLanePosition + i * laneSeparation, 0, 0) + transform.position, new Vector3(firstLanePosition + i * laneSeparation, 0, 10) + transform.position);
        }
    }
}
