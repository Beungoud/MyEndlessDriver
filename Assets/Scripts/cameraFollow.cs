using UnityEngine;
public class cameraFollow : MonoBehaviour {
    [SerializeField]
    public  Transform target;

    public FollowMode followMode = FollowMode.all;

    public enum FollowMode
    {
        Zonly, all
    }

    private Vector3 delta;
	// Use this for initialization
	void Start () {
        delta = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (followMode == FollowMode.all)
        {
            transform.position = target.position + delta;
        }
        else if (followMode == FollowMode.Zonly)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + delta.z);
        }
	}
}
