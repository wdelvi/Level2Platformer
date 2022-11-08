using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour 
{

    public bool isOnGround;

    [HideInInspector]
    public Vector2 colliderCenter;

    [HideInInspector]
    public Vector2 collisionSize;

    public void Start()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();

        if( myCollider )
        {
            collisionSize = myCollider.size;
            colliderCenter = myCollider.offset;
        }
    }

    public float GetCollisionRadiusY()
    {
        return collisionSize.y / 2.0f;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            isOnGround = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            isOnGround = false;
        }
    }
}
