using UnityEngine;
using UnityEngine.AI;

public class AIChaser : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public int health = 10;
    public int yourRange = 50;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 flatTargetPosition = new Vector3(target.position.x, 0, target.position.z);
            Vector3 flatPosition = new Vector3(transform.position.x, 0, transform.position.z);

            float distance = Vector3.Distance(flatTargetPosition, flatPosition);
            float yDifference = Mathf.Abs(transform.position.y - target.position.y);

            if (distance <= yourRange && yDifference <= 1.25f)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }
    void DecreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            BroadcastMessage("Explode");
        }
    }

    void OnParticleCollision(GameObject other)
    {
        DecreaseHealth();
    }
}