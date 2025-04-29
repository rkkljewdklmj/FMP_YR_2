using UnityEditor.Build.Content;
using UnityEngine;

    public class Enemy : MonoBehaviour
    {
        public float health = 100f; // Enemy health
        [SerializeField] private int moneyReward = 10; // Adjustable in Inspector

        void Start()
        {
            // Example usage of GameManager.Instance
            if (GameManager.Instance != null)
            {
                // Access GameManager methods or variables
                
                //Debug.Log(GameManager.Instance.someVariable);
            }
            else
            {
                Debug.LogError("GameManager instance is not found!");
            }
        }


        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }

        public void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
               
            }
        }
        void Die()
        {
            // You can add an explosion effect or sound here
            Destroy(gameObject);
        }
    }

