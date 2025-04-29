using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 10f;               // Tower shooting range
    public float fireRate = 1f;             // Time between shots
    public GameObject projectilePrefab;     // Projectile prefab
    public Transform firePoint;             // Where bullets spawn

    public bool disablefire = true;

    [Header("Tower Cost")]
    public int cost = 50;                   // 💰 Set cost in Inspector per tower prefab

    [Header("Tower Behavior")]
    [SerializeField] public bool rotateTowardsTarget = true; // 🔁 Enable/disable tower rotation

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        FindTarget(); // Look for enemies

        if (target != null)
        {
            if (rotateTowardsTarget)
            {
                RotateTowardsTarget(); // Aim at enemy only if enabled
            }

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        target = nearestEnemy != null ? nearestEnemy.transform : null;


    }

    void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        if (!disablefire)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Seek(target); // THIS is critical!
            }

      
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
