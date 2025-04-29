using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // Assign your target (e.g., base or tower)
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure target is assigned
        if (target == null)
        {
            GameObject baseObject = GameObject.FindGameObjectWithTag("Base"); // Make sure your target has the tag "Base"
            if (baseObject != null)
            {
                target = baseObject.transform;
            }
            else
            {
                Debug.LogError("No target assigned for EnemyAI. Make sure to tag your base as 'Base'.");
            }
        }

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
