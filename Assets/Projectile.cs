using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 25f;
    private Vector3 direction;
    private float lifetime;
    private float timeAlive = 0f;

    // Set the direction of the projectile
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized; // Make sure direction is normalized
    }

    public void SetLifetime(float time)
    {
        lifetime = time;  // Set the lifetime for this projectile
    }

    void Update()
    {
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);  // Destroy the projectile after it has lived for the set time
            return;
        }

        timeAlive += Time.deltaTime;

        // Move the projectile in the direction it was set to go
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);  // Destroy projectile on collision with enemy
        }
    }
}
