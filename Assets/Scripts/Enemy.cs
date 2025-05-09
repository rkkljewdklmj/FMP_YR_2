using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    [SerializeField] private int moneyReward = 10;

    private Animator animator;
    private NavMeshAgent agent; // Movement component

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (animator != null)
        {
            animator.SetBool("Walking", true); // optional
        }

        if (GameManager.Instance == null)
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

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false; // disable NavMeshAgent
        }

        // Optional: disable any other scripts like a custom movement component here

        Destroy(gameObject, 1f); // delay to let death animation play
    }
}
