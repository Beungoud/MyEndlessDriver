using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowBestScore: MonoBehaviour
{

    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int score = (int)GameManager.instance.ScoreManager.BestScore;
        text.text = score + "";


    }
}
