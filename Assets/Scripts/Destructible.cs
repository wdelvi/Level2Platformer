﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int maximumHitPoints = 3;
    public int faction = 1;
    public AudioClip soundOnHit;

    private int hitPoints;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maximumHitPoints;
    }

    public void TakeDamage( int damageAmount )
    {
        ModifyHitPoints(-damageAmount);

        if(soundOnHit != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(soundOnHit);
        }
    }

    public void Heal( int healAmount )
    {
        ModifyHitPoints(healAmount);
    }

    private void ModifyHitPoints( int modAmount )
    {
        hitPoints += modAmount;

        hitPoints = Mathf.Min(maximumHitPoints, hitPoints);

        if( hitPoints <= 0 )
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetHitPoints()
    {
        return hitPoints;
    }
}
