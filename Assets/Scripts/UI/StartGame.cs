using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
    public void StrtGame()
    {
        GameManager.instance.StartGame();
    }
}
