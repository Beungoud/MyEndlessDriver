using UnityEngine;
using System.Collections;

public class DogFlatten : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter(Collider other)
    {
        transform.localScale = new Vector3(1, 0.1f,1);
    }
}
