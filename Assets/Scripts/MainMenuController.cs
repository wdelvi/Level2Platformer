using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int startingLevelUnlocked = 1;
    public List<Button> levelSelectButtons;
    public string saveFileName = "highestLevelUnlocked";

    private int highestLevelUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        if( PlayerPrefs.HasKey(saveFileName) )
        {
            highestLevelUnlocked = PlayerPrefs.GetInt(saveFileName);
        }
        else
        {
            ResetSave();
        }

        ResetView();
    }

    // Update is called once per frame
    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt(saveFileName, highestLevelUnlocked);
        PlayerPrefs.Save();

        ResetView();
    }

    public void LoadLevel( string sceneName )
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ResetView()
    {
        for (int i = 0; i < levelSelectButtons.Count; i++)
        {
            if (highestLevelUnlocked > i)
            {
                levelSelectButtons[i].interactable = true;
            }
            else
            {
                levelSelectButtons[i].interactable = false;
            }
        }
    }

}
