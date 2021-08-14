using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScorePrevious : MonoBehaviour {

    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = (int)GameManager.instance.ScoreManager.PrevScore;
        text.text = distance + "";
    }
}
