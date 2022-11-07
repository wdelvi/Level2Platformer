using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType
{
    JumpBonus,
    MoveBonus
}

public class Collectible : MonoBehaviour
{
    public CollectibleType collectibleType;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Collector touchedCollector =
            collision.gameObject.GetComponent<Collector>();

        if(touchedCollector)
        {
            Collect(touchedCollector);
        }
    }

    private void Collect( Collector collector )
    {
        collector.Collect(collectibleType);
        Destroy(gameObject);
    }
}
