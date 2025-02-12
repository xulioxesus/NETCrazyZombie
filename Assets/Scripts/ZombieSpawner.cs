using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class ZombieSpawner : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] GameObject zombie;

    [Header("Settings")]
    [SerializeField] float spawnDelay;
    [SerializeField] int zombieMax;

    int numZombies = 0;

    public override void OnNetworkSpawn()
    {
        if(IsServer){
            InvokeRepeating(nameof(SpawnZombieRpc), 2f, spawnDelay);
        }
    }
    
    [Rpc(SendTo.Server)]
    private void SpawnZombieRpc()
    {
        if (!IsServer) return;
        
        while (numZombies < zombieMax)
        {
            GameObject enemy = Instantiate(zombie, transform.position, Quaternion.identity);
            enemy.GetComponent<NetworkObject>().Spawn();
            numZombies++;   
        }
    }
}
