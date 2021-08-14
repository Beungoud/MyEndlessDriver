using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

    public float duration;

    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;  
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > startTime + duration)
        {
            Destroy(gameObject);
        }
            
	}
}
