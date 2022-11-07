using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public List<CollectibleType> collectibles;

    public void Awake()
    {
        collectibles = new List<CollectibleType>();
    }

    public void Collect( CollectibleType collectibleType )
    {
        collectibles.Add(collectibleType);

        if(collectibleType == CollectibleType.JumpBonus)
        {
            Jumper jumper = gameObject.GetComponent<Jumper>();

            if( jumper )
            {
                jumper.StartJumpBonus(2f, 5f );
            }
        }
    }
}
