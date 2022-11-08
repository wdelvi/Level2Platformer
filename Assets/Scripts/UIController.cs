using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Destructible playerDestructible;
    public List<GameObject> heartContainers;
    public GameObject loseScreen;

    private bool hasLost = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerDestructible == null)
        {
            hasLost = true;

            Debug.LogWarning("UI Controller has no Player object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        int healthPoints = playerDestructible.GetHitPoints();

        for( int i = 0; i < heartContainers.Count; i++ )
        {
            if( i < healthPoints )
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }

        if (hasLost == false && healthPoints <= 0)
        {
            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }
            else
            {
                Debug.Log("Lose Game");
            }

            hasLost = true;
        }
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
