using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public bool allowFollowing = true;

    public Transform target;
    public float damping = 0.5f;

    public Vector3 velocity;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if( target == null || allowFollowing == false )
        {
            return;
        }

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref velocity, damping);
    }
}
