using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public Follower mainCameraFollower;
    public GameObject winScreen;
    public int levelUnlocked;
    public string saveFileName = "highestLevelUnlocked";

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
        else
        {
            Debug.Log("Won Game");
        }

        mainCameraFollower.allowFollowing = false;

        if (PlayerPrefs.HasKey(saveFileName))
        {
            int currentLevel = PlayerPrefs.GetInt(saveFileName);

            if(levelUnlocked > currentLevel)
            {
                PlayerPrefs.SetInt(saveFileName, levelUnlocked);
                PlayerPrefs.Save();
            }
        }
    }
}
