using UnityEngine;
using System.Collections;

public class CollisionDetectionMissile : MonoBehaviour {
    public GameObject explosion;
    void Start()
    {
    }

    void GameStateChanged(GameManager.GameStateEnum status)
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle") && GameManager.instance.GameState == GameManager.GameStateEnum.playing)
        {
            GameObject expl= Instantiate<GameObject>(explosion);
            expl.transform.position = other.transform.position;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
