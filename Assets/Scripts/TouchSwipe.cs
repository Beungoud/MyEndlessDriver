using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class TouchSwipe : MonoBehaviour {

    public TouchGesture.GestureSettings GestureSetting;
    private TouchGesture touch;
    void Start()
    {
        touch = new TouchGesture(this.GestureSetting);
        StartCoroutine(touch.CheckHorizontalSwipes(
            onLeftSwipe: () => {
                Debug.Log("Left");
                CrossPlatformInputManager.SetButtonDown("Left");
                CrossPlatformInputManager.SetButtonUp("Left");
            },
            onRightSwipe: () =>
            {
                Debug.Log("Right");
                CrossPlatformInputManager.SetButtonDown("Right");
                CrossPlatformInputManager.SetButtonUp("Right");
            },
             onUpSwipe: () => {
                 Debug.Log("Up");
                 CrossPlatformInputManager.SetButtonDown("Up");
                 CrossPlatformInputManager.SetButtonUp("Up");
             },
             onDownSwipe: () => {
                 Debug.Log("Down");
                  },
            onPress: () =>
            {
                Debug.Log("Press");
                CrossPlatformInputManager.SetButtonDown("Down");
            },
            onRelease: () =>
            {
                Debug.Log("Release");
                CrossPlatformInputManager.SetButtonUp("Down");
            }
            ));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
