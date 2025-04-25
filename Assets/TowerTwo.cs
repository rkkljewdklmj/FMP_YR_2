using UnityEngine;

public class TowerTwo : MonoBehaviour
{
    public float fireRate = 1f;                // Time between shots
    public GameObject projectilePrefab;        // Projectile prefab (assign in Inspector)
    public Transform firePoint;                // Where bullets spawn
    public bool disablefire = true;
    public int numberOfProjectiles = 8;        // Number of projectiles to shoot in a circle
    public float projectileSpreadRadius = 1f;  // Radius from center for spawn direction
    public float projectileLifetime = 3f;      // Time after which the projectile will self-destruct
    public float range = 10f;                  // Tower shooting range

    [Header("Tower Cost")]
    public int cost = 100;                     // Set the cost of TOWERTWO here

    private float fireCountdown = 0f;

    void Update()
    {
        if (!disablefire)
        {
            if (fireCountdown <= 0f)
            {
                // Check if there's any enemy within range
                if (IsEnemyInRange())
                {
                    ShootInCircle();
                }
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    bool IsEnemyInRange()
    {
        // Check if there are any enemies within the range of the tower
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));
        return enemies.Length > 0;
    }

    void ShootInCircle()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirZ = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 shootDir = new Vector3(projectileDirX, 0, projectileDirZ);
            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(shootDir));

            // Optionally set direction if your projectile uses a method like SetDirection
            if (projectileGO.TryGetComponent(out Projectile projectile))
            {
                projectile.SetDirection(shootDir);  // Set the direction for each projectile
                projectile.SetLifetime(projectileLifetime);  // Set the lifetime of the projectile
            }

            angle += angleStep;
        }
    }
}
