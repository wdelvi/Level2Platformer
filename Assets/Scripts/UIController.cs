using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Destructible playerDestructible;
    public List<GameObject> heartContainers;

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
    }
}
