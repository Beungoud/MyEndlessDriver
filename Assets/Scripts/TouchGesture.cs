

using System;
using System.Collections;
using UnityEngine;

public class TouchGesture
{
    [System.Serializable]
    public class GestureSettings
    {
        public float minSwipeDist = 100;
        public float maxSwipeTime = 10;
    }
    private GestureSettings settings;
    private float swipeStartTime;
    private bool couldBeSwipe;
    private Vector2 startPos;
    private int stationaryForFrames;
    private TouchPhase lastPhase;
    private bool wasPressed;

    public TouchGesture(GestureSettings gestureSettings)
    {
        this.settings = gestureSettings;
    }
    public IEnumerator CheckHorizontalSwipes(Action onLeftSwipe, Action onRightSwipe, Action onUpSwipe, Action onDownSwipe, Action onPress, Action onRelease) //Coroutine, which gets Started in "Start()" and runs over the whole game to check for swipes
    {
        while (true)
        { //Loop. Otherwise we wouldnt check continuously ;-)
            foreach (Touch touch in Input.touches)
            { //For every touch in the Input.touches - array...
                switch (touch.phase)
                {
                    case TouchPhase.Began: //The finger first touched the screen --> It could be(come) a swipe
                        couldBeSwipe = true;
                        startPos = touch.position;  //Position where the touch started
                        swipeStartTime = Time.time; //The time it started
                        stationaryForFrames = 0;
                        wasPressed = false;
                        break;


                    case TouchPhase.Stationary: //Is the touch stationary? --> No swipe then!
                        if (isContinouslyStationary(frames: 6))
                        {
                            couldBeSwipe = false;
                            if (!wasPressed)
                            {
                                onPress();
                                wasPressed = true;
                            }
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Moved:
                        if (isHSwipe(touch))
                        {
                            couldBeSwipe = false; //<-- Otherwise this part would be called over and over again.
                            if (Mathf.Sign(touch.position.x - startPos.x) == 1f) //Swipe-direction, either 1 or -1.   
                                onRightSwipe(); //Right-swipe
                            else
                                onLeftSwipe(); //Left-swipe
                        }
                        else if (isVSwipe(touch))
                        {
                            couldBeSwipe = false; //<-- Otherwise this part would be called over and over again.
                            if (Mathf.Sign(touch.position.y - startPos.y) == 1f) //Swipe-direction, either 1 or -1.   
                                onUpSwipe(); //Right-swipe
                            else
                                onDownSwipe(); //Left-swipe
                        }
                        break;
                }
                if (touch.phase == TouchPhase.Ended && wasPressed)
                {
                    onRelease();
                }
                lastPhase = touch.phase;
            }
            yield return null;
        }
    }
    private bool isContinouslyStationary(int frames)
    {
        if (lastPhase == TouchPhase.Stationary)
            stationaryForFrames++;
        else // reset back to 1
            stationaryForFrames = 1;
        return stationaryForFrames > frames;
    }
    private bool isHSwipe(Touch touch)
    {
        float swipeTime = Time.time - swipeStartTime; //Time the touch stayed at the screen till now.
        float swipeDistH = Mathf.Abs(touch.position.x - startPos.x); //Swipe distance
        float swipeDistV = Mathf.Abs(touch.position.y - startPos.y); //Swipe distance

        return couldBeSwipe && swipeTime < settings.maxSwipeTime && swipeDistH > settings.minSwipeDist && swipeDistH > swipeDistV;
    }
    private bool isVSwipe(Touch touch)
    {
        float swipeTime = Time.time - swipeStartTime; //Time the touch stayed at the screen till now.
        float swipeDistH = Mathf.Abs(touch.position.x - startPos.x); //Swipe distance
        float swipeDistV = Mathf.Abs(touch.position.y - startPos.y); //Swipe distance
        return couldBeSwipe && swipeTime < settings.maxSwipeTime && swipeDistV > settings.minSwipeDist && swipeDistV > swipeDistH;
    }
}