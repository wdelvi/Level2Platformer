using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip pickupSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController =
            collision.gameObject.GetComponent<PlayerController>();

        if (playerController && UIController.Instance != null)
        {
            OnPickup();
        }
    }

    public void OnPickup()
    {
        UIController.Instance.ModifyCoinCount(coinValue);

        if(GetComponent<AudioSource>() && pickupSound)
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
