using UnityEngine;
using System.Collections;
using System;
/*using GooglePlayGames;
*/
public class ShowLeaderboard : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameManager.instance.googlePlayConnectedChanged += GooglePlayConnectedChanged;
        GooglePlayConnectedChanged(GameManager.instance.GooglePlayConnected);
    }

    private void GooglePlayConnectedChanged(bool status)
    {
        gameObject.SetActive(status);
    }


    public void ShowLeaderoard()
    {
        /*
                 if (GameManager.instance.GooglePlayConnected)
                {
                    if (Social.Active is PlayGamesPlatform)
                    {
                        PlayGamesPlatform playGamesPlatform = (PlayGamesPlatform)Social.Active;
                        playGamesPlatform.ShowLeaderboardUI(PlayServiceConstants.leaderboard_high_score);
                    } else
                    {
                        Social.Active.ShowLeaderboardUI();
                    }
                }*/
    }

}
