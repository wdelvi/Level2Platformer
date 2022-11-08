using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingController : MonoBehaviour 
{
    
    public float looksAheadDistance = 0.5f;
    public float looksDownwardDistance = 1f;
    public LayerMask raycastLayermask;

    private SpriteRenderer spriteRenderer;
    private Mover controlledMover;
    private float movementDirection;

    private RaycastHit hit;

	// Use this for initialization
	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controlledMover = GetComponent<Mover>();
        movementDirection = -1.0f;
	}

    public void OnDrawGizmos()
    {
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.x += looksAheadDistance * movementDirection;

        Gizmos.color = Color.red;
        Vector3 raycastDistance = -Vector3.up;
        raycastDistance.y = transform.position.y - looksDownwardDistance;

        Gizmos.DrawRay(raycastOrigin, raycastDistance);
    }

    // Update is called once per frame
    void Update () 
    {
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.x += looksAheadDistance * movementDirection;

        if (Physics2D.Raycast(raycastOrigin, -Vector3.up, looksDownwardDistance, raycastLayermask))
        {
            controlledMover.AccelerateInDirection(new Vector3(movementDirection, 0f, 0f));
        }
        else
        {
            movementDirection *= -1f;

            if(spriteRenderer != null)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
	}
}
