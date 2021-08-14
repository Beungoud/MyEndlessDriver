using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ShowCurrentLevel : MonoBehaviour {
    Animator animator;
    Text text;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
        GameManager.instance.LevelManager.levelChanged += LevelChanged;
	}

    private void LevelChanged(int level)
    {
        animator.SetTrigger("NextLevel");
        text.text = GameManager.instance.LevelManager.CurrentLevelDefinition.name;
    }
}
