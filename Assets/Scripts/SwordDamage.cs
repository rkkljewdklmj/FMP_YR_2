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
            Vector3 spawnPos = new Vector3(firePoint.transform.position.x, 2.2f, firePoint.transform.position.z); // raise by 1 unit
            Instantiate(swordPrefab, spawnPos, firePoint.transform.rotation);

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
