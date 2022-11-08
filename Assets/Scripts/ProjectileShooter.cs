using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public int startingAmmo = 10;
    public GameObject projectilePrefab;

    [Tooltip("A child object placed where the projectile will spawn from")]
    public Transform spawnPoint;

    private Vector3 direction;
    private int currentAmmo;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void Start()
    {
        currentAmmo = startingAmmo;

        if (UIController.Instance)
        {
            UIController.Instance.SetProjectileCount(currentAmmo);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (currentAmmo > 0 || startingAmmo < 0)
        {
            ModifyAmmoCount(-1);

            GameObject newProjectile = Instantiate(projectilePrefab) as GameObject;

            newProjectile.transform.position = spawnPoint.position;

            ProjectileController newProjectileController = newProjectile.GetComponent<ProjectileController>();

            if (newProjectileController != null)
            {
                newProjectileController.Setup(direction);
            }
            else
            {
                Debug.LogWarning("Projectile is missing a projectile controller!");
            }
        }
    }

    public void ModifyAmmoCount( int ammoChange )
    {
        currentAmmo += ammoChange;

        if( UIController.Instance )
        {
            UIController.Instance.SetProjectileCount(currentAmmo);
        }
    }
}
