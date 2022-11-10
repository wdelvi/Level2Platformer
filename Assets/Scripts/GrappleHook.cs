using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a distance joint and a line renderer to work
[RequireComponent(typeof(DistanceJoint2D))]
[RequireComponent(typeof(LineRenderer))]

public class GrappleHook : MonoBehaviour
{
    [Tooltip("How far away we should be allowed to grapple things")]
    public float maxGrappleDistance = 10f;

    [Tooltip("Which things, chosen by layer, should we be allowed to grapple")]
    public LayerMask grappleMask;

    [Tooltip("Which audio clip to play if we have an Audio Source")]
    public AudioClip grappleSound;

    //The position we are currently grappled to
    private Vector3 targetPosition;

    //Reference variables to our distance joint and our line renderer
    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;

    //Draw gizmos is 100% a debug function. Players WILL NOT see it. It is only to help us design
    public void OnDrawGizmos()
    {
        //Draw a red sphere around wherever we are allowed to grapple
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position, maxGrappleDistance );

        //Draw a yellow line where the grapple currently is
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, (targetPosition - transform.position) );
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get references to our distance joint and line renderer
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();

        //Turn them both off until we need them
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //On left click, start a grapple
        if( Input.GetKeyDown(KeyCode.Mouse0) )
        {
            StartGrapple();
        }

        //On release left click, end the grapple
        if( Input.GetKeyUp(KeyCode.Mouse0) )
        {
            EndGrapple();
        }

        //While key is held down, continue the grapple
        if( Input.GetKey(KeyCode.Mouse0) )
        {
            ContinueGrapple();
        }
    }

    public void StartGrapple()
    {
        //Get the world position of wherever you clicked the mouse
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        //Raycast to that position to see if there is something in the way
        RaycastHit2D raycastHit =
            Physics2D.Raycast(transform.position, (targetPosition - transform.position), maxGrappleDistance, grappleMask);

        //If we hit something...
        if( raycastHit.collider && raycastHit.collider.GetComponent<Rigidbody2D>() )
        {
            //Turn on our distance joint...
            distanceJoint.enabled = true;

            //Connect us to the thing we hit...
            distanceJoint.connectedBody = raycastHit.collider.GetComponent<Rigidbody2D>();

            //Set the correct distance...
            distanceJoint.distance = Vector2.Distance(transform.position, raycastHit.point);

            //Set the correct anchor...
            distanceJoint.connectedAnchor = raycastHit.point -
                new Vector2(raycastHit.collider.transform.position.x, raycastHit.collider.transform.position.y);

            //Turn on our line renderer...
            lineRenderer.enabled = true;

            //Calculate the start position...
            Vector3 firstGrapplePosition = transform.position;
            firstGrapplePosition.z = transform.position.z + 1f;

            //Set the start position of the line...
            lineRenderer.SetPosition(0, firstGrapplePosition);

            ///Calculate the end position...
            Vector3 toPosition = raycastHit.point;
            toPosition.z = transform.position.z + 1f;

            //Set the end position of the line...
            lineRenderer.SetPosition(1, toPosition);

            //If we have an audio source and a sound, play the sound
            if (GetComponent<AudioSource>() != null && grappleSound != null)
            {
                GetComponent<AudioSource>().PlayOneShot(grappleSound);
            }
        }
    }

    public void EndGrapple()
    {
        //When we finish, all we have to do is turn off the line renderer and distance joint
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    public void ContinueGrapple()
    {
        //While we move, all we have to do is modify the start position of the line
        lineRenderer.SetPosition(0, transform.position);
    }
}
