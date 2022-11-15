using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Tooltip("How much ammo this shooter should have, less than 0 is infinite")]
    public int startingAmmo = 10;
    [Tooltip("A prefab with a projectile controller. Which projectile we shoot.")]
    public GameObject projectilePrefab;
    [Tooltip("Whether or not we should update the player's ammo count UI")]
    public bool consumePlayerAmmo = true;
    [Tooltip("A child object placed where the projectile will spawn from")]
    public Transform spawnPoint;

    //Which direction the projectile should go
    private Vector3 direction;
    //How much ammo we currently have
    private int currentAmmo;

    //A setter function to tell the shooter which direction to shoot
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void Start()
    {
        //Set our starting ammo
        currentAmmo = startingAmmo;

        //If we have a ui controller and we are told to, update ammo UI
        if (UIController.Instance && consumePlayerAmmo)
        {
            UIController.Instance.SetProjectileCount(currentAmmo);
        }
    }

    public void Update()
    {
        //Listen for input every frame
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //If input is heard, shoot
            Fire();
        }
    }

    public void Fire()
    {
        //If we still have ammo or if ammo is infinite, allow shooting
        if (currentAmmo > 0 || startingAmmo < 0)
        {
            //Lose one ammo
            ModifyAmmoCount(-1);

            //Instantiate (or spawn) a new projectile based on our prefab
            GameObject newProjectile = Instantiate(projectilePrefab) as GameObject;

            //Set the position of the spawned projectile to the spawn point
            newProjectile.transform.position = spawnPoint.position;

            //Check the spawned projectile for a projectile controller
            ProjectileController newProjectileController = newProjectile.GetComponent<ProjectileController>();

            //If we have one...
            if (newProjectileController != null)
            {
                //Set it up
                newProjectileController.Setup(direction);
            }
            else
            {
                //If we don't, let the Game Dev know they're missing one
                Debug.LogWarning("Projectile is missing a projectile controller!");
            }
        }
    }

    public void ModifyAmmoCount( int ammoChange )
    {
        //Change ammo depending on the ammo change, positive or negative
        currentAmmo += ammoChange;

        //If we have a ui controller and we are told to, update ammo UI
        if ( UIController.Instance && consumePlayerAmmo)
        {
            UIController.Instance.SetProjectileCount(currentAmmo);
        }
    }
}
