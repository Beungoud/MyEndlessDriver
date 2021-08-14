using UnityEngine;
using System.Collections;

public class AutoRecenter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameManager.instance.stepBack += stepBack;
    }

    public void OnDestroy()
    {
        GameManager.instance.stepBack -= stepBack;
    }

    void stepBack(float distance)
    {
       transform.position = transform.position - Vector3.forward * distance;
    }
}
