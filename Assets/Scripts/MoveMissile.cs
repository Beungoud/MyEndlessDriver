using UnityEngine;
using System.Collections;

public class MoveMissile : MonoBehaviour {
    public float missileSpeed=8;

    public Transform aliveLimit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.position.z > aliveLimit.position.z)
        {
            Destroy(gameObject);
        }
        transform.position=transform.position + Vector3.forward * missileSpeed;
	}
}

