using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectScreenOrientation : MonoBehaviour
{
    public GameObject portraitView;
    public GameObject landscapeView;
    public Text debugtext;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debugtext != null)
        {
            debugtext.text = "Orientation " + Screen.orientation;
        }
        //Debug.Log("Orientation " + Screen.orientation);
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            portraitView.SetActive(true);
            landscapeView.SetActive(false);
        } else
        {
            portraitView.SetActive(false);
            landscapeView.SetActive(true);
        }
    }
}