using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class StartButtonAutoAssign : MonoBehaviour
{

    [SerializeField]
    private EventSystem es;
 

    public void Awake()
    {
        es = FindObjectOfType<EventSystem>();

    }

    private void OnEnable()
    {
        Debug.Log("Startbutton enabled");
        es.SetSelectedGameObject(gameObject);
    }
}
