using UnityEngine;

public class ProjectileTowerTwo : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 25f;
    public float lifetime = 3f; // How long the projectile lasts

    private Vector3 direction;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after set time
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
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

            Destroy(gameObject); // Destroy projectile on hit
        }
    }
}
