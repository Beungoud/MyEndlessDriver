using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShowScore : MonoBehaviour {

    private Text text;

    public Animator coinAnim;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        GameManager.instance.ScoreManager.coinCountChanged += CoinCountchanged;
	}

    private void CoinCountchanged(int coinCount)
    {
        Debug.Log("NewCoin");
        Animator anim = Instantiate<Animator>(coinAnim);
        anim.transform.parent = transform.parent;
        anim.transform.position = transform.position;
        anim.Play("NewCoin");
    }

    // Update is called once per frame
    void Update () {
        int score = (int)GameManager.instance.ScoreManager.Score;
        text.text = ""+ score;
        if (score > GameManager.instance.ScoreManager.BestScore)
        {
            text.color = Color.red;
        } else
        {
            text.color = Color.white;
        }

    }
}
