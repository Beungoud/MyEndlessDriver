using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
    public GameObject explosion;

    void Start()
    {
        GameManager.instance.gameStateChanged += GameStateChanged;
    }

    void GameStateChanged(GameManager.GameStateEnum status)
    {
        if (status == GameManager.GameStateEnum.playing)
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle") && GameManager.instance.GameState == GameManager.GameStateEnum.playing)
        {
//            Destroy(other.gameObject);
            GameManager.instance.PlayerDied();
            GetComponentInChildren<MeshRenderer>().enabled = false;

            GameObject expl = GameObject.Instantiate(explosion);
            expl.transform.position = transform.position;
            expl.GetComponent<AutoRecenter>().enabled = false;
            expl.GetComponentInChildren<ParticleSystem>().playbackSpeed = 2;
            expl.transform.parent = transform;
        }
        if (other.gameObject.CompareTag("Bonus") && GameManager.instance.GameState == GameManager.GameStateEnum.playing)
        {
            BonusDefinition bonnusDef = other.GetComponent<BonusDefinition>();
            switch(bonnusDef.Type)
            {
                case BonusDefinition.TypeEnum.missile:
                    Destroy(other.gameObject);
                    GameManager.instance.MissileCount++;
                    break;
                case BonusDefinition.TypeEnum.dog:
                    Debug.Log("Wouf");
                    break;
                case BonusDefinition.TypeEnum.coin:
                    Destroy(other.gameObject);
                    GameManager.instance.ScoreManager.CoinCount++;
                    break;
            }
        }
    }
}
