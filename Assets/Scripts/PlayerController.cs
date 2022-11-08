using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float airControl = 0.5f;
    private Mover mover;
    private Jumper jumper;
    private Animator animator;
    private ProjectileShooter projectileShooter;

    void Start()
    {
        mover = gameObject.GetComponent<Mover>();
        jumper = gameObject.GetComponent<Jumper>();
        animator = gameObject.GetComponent<Animator>();

        projectileShooter = GetComponent<ProjectileShooter>();

        if (projectileShooter != null)
        {
            projectileShooter.SetDirection(new Vector2(1, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("IsOnGround", jumper.GetIsOnGround());
        animator.SetFloat("YVelocity", gameObject.GetComponent<Rigidbody2D>().velocity.y);

        float airControlModifier = jumper.GetIsOnGround() ? 1f : airControl;

        //Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            mover.AccelerateInDirection(new Vector2(airControlModifier, 0f));
            animator.SetBool("Walking", true);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);

            if (projectileShooter != null)
            {
                projectileShooter.SetDirection(new Vector2(1f, 0.1f));
            }
        }

        //Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            mover.AccelerateInDirection(new Vector2(-airControlModifier, 0f));
            animator.SetBool("Walking", true);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);

            if (projectileShooter != null)
            {
                projectileShooter.SetDirection(new Vector2(-1f, 0.1f));
            }
        }

        //Jump
        if ( Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )
        {
            jumper.Jump();
        }
    }
}
