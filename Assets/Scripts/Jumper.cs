using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpImpulse = 5f;

    public float jumpImpulseModifier = 1f;

    private bool isOnGround;

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (isOnGround == true)
        {
            myRigidbody.velocity =
                new Vector2(myRigidbody.velocity.x, jumpImpulse * jumpImpulseModifier);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        isOnGround = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;
    }

    public bool GetIsOnGround()
    {
        return isOnGround;
    }

    public void StartJumpBonus( float newJumpModifier, float bonusLength )
    {
        jumpImpulseModifier = newJumpModifier;
        CancelInvoke();
        Invoke("EndJumpBonus", bonusLength);
    }

    public void EndJumpBonus()
    {
        jumpImpulseModifier = 1f;
    }
}
