using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f; // Enemy health

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // You can add an explosion effect or sound here
        Destroy(gameObject);
    }
}
