using UnityEngine;
using UnityEngine.AI;

public class MoveToPosition : MonoBehaviour
{
    Transform target;

    NavMeshAgent agent;
    
    void Start()
    {
        // referencia al jugador
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (!agent.isStopped)
        {
            agent.SetDestination(target.position);
        }
    }
}
