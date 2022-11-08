using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoValue = 5;
    public ProjectileShooter trackedShooter;
    public AudioClip pickupSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController =
            collision.gameObject.GetComponent<PlayerController>();

        if (playerController && trackedShooter != null)
        {
            OnPickup();
        }
    }

    public void OnPickup()
    {
        trackedShooter.ModifyAmmoCount(ammoValue);

        if (GetComponent<AudioSource>() && pickupSound)
        {
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
        }

        GetComponent<Collider2D>().enabled = false;

        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        Invoke("Die", 5f);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
