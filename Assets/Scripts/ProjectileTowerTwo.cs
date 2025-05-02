using UnityEngine;

public class ProjectileTowerTwo : MonoBehaviour
{
    public float speed = 10f;              // How fast the projectile moves
    public float damage = 25f;             // Damage dealt to the enemy on hit
    public float lifetime = 3f;            // Time in seconds before the projectile is automatically destroyed

    private Vector3 direction;             // Direction the projectile will move in

    void Start()
    {
        // Destroy this projectile after 'lifetime' seconds to avoid it staying in the scene forever
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile in the specified direction every frame
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // This method is called externally to set the projectile's movement direction
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;        // Normalize to ensure consistent speed regardless of vector length
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the projectile collided with an object tagged "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Try to get the Enemy script from the collided object
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Apply damage to the enemy
            }

            Destroy(gameObject);           // Destroy the projectile after hitting an enemy
        }
    }
}
