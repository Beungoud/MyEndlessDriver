using UnityEngine;
using System.Collections;

public class FireMissile : MonoBehaviour {

    public GameObject missile;

    public Transform aliveLimit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown("Up") || Input.GetButtonDown("Up") || Input.GetButtonDown("Fire"))
        {
            if (GameManager.instance.MissileCount > 0)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        GameManager.instance.MissileCount--;
        GameObject newGo = Instantiate<GameObject>(missile);
        MoveMissile mis = newGo.GetComponent<MoveMissile>();
        mis.aliveLimit = aliveLimit;
        newGo.transform.position = transform.position;
    }

}
