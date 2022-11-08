using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float maxGrappleDistance = 10f;
    public LayerMask grappleMask;
    public AudioClip jumpSound;

    private Vector3 targetPosition;
    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position, maxGrappleDistance );

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, (targetPosition - transform.position) );
    }

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();

        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Mouse0) )
        {
            StartGrapple();
        }

        if( Input.GetKeyUp(KeyCode.Mouse0) )
        {
            EndGrapple();
        }

        if( Input.GetKey(KeyCode.Mouse0) )
        {
            ContinueGrapple();
        }
    }

    public void StartGrapple()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        RaycastHit2D raycastHit =
            Physics2D.Raycast(transform.position, (targetPosition - transform.position), maxGrappleDistance, grappleMask);

        if( raycastHit.collider && raycastHit.collider.GetComponent<Rigidbody2D>() )
        {
            distanceJoint.enabled = true;

            distanceJoint.connectedBody = raycastHit.collider.GetComponent<Rigidbody2D>();

            distanceJoint.distance = Vector2.Distance(transform.position, raycastHit.point);

            distanceJoint.connectedAnchor = raycastHit.point -
                new Vector2(raycastHit.collider.transform.position.x, raycastHit.collider.transform.position.y);

            lineRenderer.enabled = true;

            Vector3 firstGrapplePosition = transform.position;
            firstGrapplePosition.z = transform.position.z + 1f;

            lineRenderer.SetPosition(0, firstGrapplePosition);

            Vector3 toPosition = raycastHit.point;
            toPosition.z = transform.position.z + 1f;
            lineRenderer.SetPosition(1, toPosition);

            if (GetComponent<AudioSource>() != null && jumpSound != null)
            {
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }
        }
    }

    public void EndGrapple()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    public void ContinueGrapple()
    {
        lineRenderer.SetPosition(0, transform.position);
    }
}
