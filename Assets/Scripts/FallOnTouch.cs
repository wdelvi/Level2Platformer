using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnTouch : MonoBehaviour 
{
    public float timeToDrop = 3f;

    private float dropTimer;
    private bool shouldDrop;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.collider.gameObject.tag != "Player" )
        {
            return;
        }

        float collisionY = collision.collider.transform.position.y - collision.collider.bounds.size.y / 2;

        if ( transform.position.y < collisionY )
        {
            shouldDrop = true;
        }
    }

    public void Update()
    {
        if( shouldDrop )
        {
            dropTimer += Time.deltaTime;

            if( dropTimer > timeToDrop )
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
