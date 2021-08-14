using UnityEngine;
using System.Collections;

public class BonusDestroy : MonoBehaviour {
    public Transform destroyLimit;


    public void Update()
    {
        if (destroyLimit.position.z > transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
