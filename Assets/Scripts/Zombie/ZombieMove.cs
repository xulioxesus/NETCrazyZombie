using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;
using Unity.Services.Matchmaker.Models;

public class ZombieMove : NetworkBehaviour
{
    Transform target;

    NavMeshAgent agent;
    
    public override void OnNetworkSpawn()
    {
        if(IsServer){
            agent = GetComponent<NavMeshAgent>();
            Invoke("FindTarget",3);
        }


    }
    
    void Update()
    {
        if (!IsServer) return; // Solo el servidor controla el movimiento del enemigo

        if (!agent.isStopped && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            target = players[Random.Range(0, players.Length)].transform;
        }
    }
}
