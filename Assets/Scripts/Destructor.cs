using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    public int damage = 1;
    public int faction = 1;
    public float knockbackForce = 0f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destructible destrucible =
            collision.gameObject.GetComponent<Destructible>();

        if( destrucible && destrucible.faction != faction )
        {
            destrucible.TakeDamage(damage);

            Vector3 knockbackVector = collision.transform.position - transform.position;
            if (collision.gameObject.GetComponent<Rigidbody2D>())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(knockbackVector * knockbackForce, transform.position);
            }
        }
    }
}
