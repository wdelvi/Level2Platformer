using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Mover mover;
    private Jumper jumper;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        mover = gameObject.GetComponent<Mover>();
        jumper = gameObject.GetComponent<Jumper>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("IsOnGround", jumper.GetIsOnGround());
        animator.SetFloat("YVelocity", gameObject.GetComponent<Rigidbody2D>().velocity.y);

        //Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            mover.AccelerateInDirection(new Vector2(1f, 0f));
            animator.SetBool("Walking", true);
            spriteRenderer.flipX = false;
        }

        //Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            mover.AccelerateInDirection(new Vector2(-1f, 0f));
            animator.SetBool("Walking", true);
            spriteRenderer.flipX = true;
        }

        //Jump
        if( Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )
        {
            jumper.Jump();
        }
    }
}
