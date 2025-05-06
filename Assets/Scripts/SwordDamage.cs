using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public float damage = 20f;

    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    GameObject firePoint;

    public GameObject swordPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
           

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
