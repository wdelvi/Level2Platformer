using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jumper))]
public class JumpingEnemyController : MonoBehaviour
{
    public float timeBetweenJumps = 5f;

    private float jumpTimer;
    private Jumper jumper;

    // Start is called before the first frame update
    void Start()
    {
        jumpTimer = 0f;
        jumper = gameObject.GetComponent<Jumper>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer += Time.deltaTime;

        if( jumpTimer >= timeBetweenJumps )
        {
            jumper.Jump();
            jumpTimer = 0f;
        }
    }
}
