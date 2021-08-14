using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMissileCount : MonoBehaviour {

    private Text text;
    public Animator newMissileAnim;
    private int prevMissileCount;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        GameManager.instance.missileCountChanged += MissileCountChanged;
        prevMissileCount = GameManager.instance.MissileCount;
        MissileCountChanged(GameManager.instance.MissileCount);
    }

    void MissileCountChanged(int missileCount)
    {
        text.text = "" + missileCount;
        if (prevMissileCount < missileCount)
        {
            newMissileAnim.SetTrigger("newOpt");
        }
        prevMissileCount = missileCount;
    }
}
