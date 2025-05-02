using UnityEngine;

public class TowerTwo : MonoBehaviour
{
    [Header("Tower Settings")]
    public float fireRate = 1f;                    // How often the tower fires
    public float range = 10f;                      // Radius in which enemies can be detected
    public int cost = 100;                         // Cost of placing this tower
    public bool disablefire = true;                // Set to false to allow firing
    public GameObject effectPrefab;
    
    [Header("Animation")]
    public Animator animator;  // Assign this in the Inspector

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;            // Assign the ProjectileTowerTwo prefab
    public Transform firePoint;                    // Transform where projectiles are spawned from
    public int numberOfProjectiles = 8;            // How many projectiles to fire in a circle
    public float projectileLifetime = 3f;          // How long each projectile lasts before being destroyed

    private float fireCountdown = 0f;              // Timer between shots

    public  ParticleSystem particleSystem;

    void Update()
    {
        // Skip if firing is disabled
        if (disablefire) return;

        // Countdown between shots
        if (fireCountdown <= 0.1f)
        {
            bool enemyFound = IsEnemyInRange();    // Check for enemies within range
            Debug.Log("Enemy in range: " + enemyFound);

            if (enemyFound)
            {
                ShootInCircle();                   // Fire projectiles if an enemy is nearby
                fireCountdown = 1f / fireRate;     // Reset countdown based on fire rate
            }
        }

        fireCountdown -= Time.deltaTime;           // Decrease the countdown timer
    }

    // Check if any enemy is within the tower's range using physics overlap
    bool IsEnemyInRange()
    {
       
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));
        return enemies.Length > 0;
    }

    // Shoots projectiles in evenly spaced directions in a 360° circle
    void ShootInCircle()
    {
        float angleStep = 360f / numberOfProjectiles;  // Angle between each projectile
        float angle = 0f;
       
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        Debug.Log("Shooting in circle");

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calculate direction based on current angle
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirZ = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 shootDir = new Vector3(dirX, 0, dirZ);

            // Instantiate the projectile at the firePoint and rotate it toward the direction
            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(shootDir));
            Debug.Log("Projectile instantiated: " + projectileGO.name);

            // Apply direction and lifetime settings to the projectile
            if (projectileGO.TryGetComponent(out ProjectileTowerTwo proj))
            {
                proj.SetDirection(shootDir);
                proj.lifetime = projectileLifetime;
            }

            angle += angleStep; // Move to next direction angle
        }
    }

    public void PlayEffect()
    {
        {
            if (effectPrefab != null)
            {

                particleSystem.Play();
               // Instantiate(effectPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // Draws a visible wireframe sphere in the Unity editor for tower range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
