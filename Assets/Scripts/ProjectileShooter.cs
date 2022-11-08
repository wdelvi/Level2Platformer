using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;

    [Tooltip("A child object placed where the projectile will spawn from")]
    public Transform spawnPoint;

    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
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
        GameObject newProjectile = Instantiate(projectilePrefab) as GameObject;

        newProjectile.transform.position = spawnPoint.position;

        ProjectileController newProjectileController = newProjectile.GetComponent<ProjectileController>();

        if(newProjectileController != null)
        {
            newProjectileController.Setup(direction);
        }
        else
        {
            Debug.LogWarning("Projectile is missing a projectile controller!");
        }
    }
}
