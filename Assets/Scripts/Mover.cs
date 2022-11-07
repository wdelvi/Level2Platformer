using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float acceleration = 5f;

    public float maximumSpeed = 10f;

    private Rigidbody2D myRigidbody;

    public void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void AccelerateInDirection( Vector2 direction )
    {
        Vector2 newVelocity = myRigidbody.velocity
            + (direction * acceleration * Time.deltaTime);

        newVelocity.x =
            Mathf.Clamp(newVelocity.x, -maximumSpeed, maximumSpeed);

        myRigidbody.velocity = newVelocity;
    }
}
