using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public Follower mainCameraFollower;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Won Game");

        mainCameraFollower.allowFollowing = false;
    }
}
