using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public Destructible playerDestructible;
    public List<GameObject> heartContainers;
    public GameObject loseScreen;
    public TMP_Text coinCountText;
    public TMP_Text projectileCountText;

    private bool hasLost = false;
    private int coinCount;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerDestructible == null)
        {
            hasLost = true;

            Debug.LogWarning("UI Controller has no Player object");
        }

        coinCount = 0;
        ModifyCoinCount(0);
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

    public void ModifyCoinCount(int numCoins)
    {
        coinCount += numCoins;

        if (coinCountText != null)
        {
            coinCountText.text = "X " + coinCount;
        }
    }

    public void SetProjectileCount(int totalProjectiles)
    {
        if (projectileCountText != null)
        {
            projectileCountText.text = "X " + totalProjectiles;
        }
    }
}
