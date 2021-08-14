using UnityEngine;
using System.Collections;

public class ShowGameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.instance.gameStateChanged += GameStateChanged;
        GameStateChanged(GameManager.instance.GameState);
    }

    void GameStateChanged(GameManager.GameStateEnum status)
    {
        Debug.Log("GameStateChanged");
        if (status == GameManager.GameStateEnum.menu_start || status == GameManager.GameStateEnum.menu_score || status == GameManager.GameStateEnum.menu_bestscore)
        {
            gameObject.SetActive(true);
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
