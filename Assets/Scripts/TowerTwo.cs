using UnityEngine;

public class TowerTwo : MonoBehaviour
{
    [Header("Tower Settings")]
    public float fireRate = 1f;
    public float range = 10f;
    public int cost = 100;
    public bool disablefire = true;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;        // Assign ProjectileTowerTwo prefab
    public Transform firePoint;                // Where the bullets spawn from
    public int numberOfProjectiles = 8;        // How many bullets to shoot
    public float projectileLifetime = 3f;      // How long before bullets auto-destroy

    private float fireCountdown = 0f;

    void Update()
    {
        if (fireCountdown <= 0f)
        {
            bool enemyFound = IsEnemyInRange();
            Debug.Log("Enemy in range: " + enemyFound);
            Debug.Log("Projectile moving");
           

            if (enemyFound)
            {
                ShootInCircle();
                fireCountdown = 1f / fireRate;
            }
        }
    }

    bool IsEnemyInRange()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));
        return enemies.Length > 0;
    }

    void ShootInCircle()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;
        Debug.Log("Shooting in circle");
       
       


        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirZ = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 shootDir = new Vector3(dirX, 0, dirZ);

            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(shootDir));
            Debug.Log("Projectile instantiated: " + projectileGO.name);
            if (projectileGO.TryGetComponent(out ProjectileTowerTwo proj))
            {
                proj.SetDirection(shootDir);
                proj.lifetime = projectileLifetime;
            }

            angle += angleStep;
        }
    }

    // Optional: Visualize range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
