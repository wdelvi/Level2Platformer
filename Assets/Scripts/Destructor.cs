using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    public int damage = 1;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destructible destrucible =
            collision.gameObject.GetComponent<Destructible>();

        if( destrucible )
        {
            destrucible.TakeDamage(damage);
        }
    }
}
