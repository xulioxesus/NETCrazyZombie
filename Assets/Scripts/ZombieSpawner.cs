using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject zombie;

    [Header("Settings")]
    [SerializeField] float spawnDelay;
    [SerializeField] int zombieMax;

    int numZombies = 0;

    void Start()
    {
        StartCoroutine(SpawnZombie());
    }
    
    IEnumerator SpawnZombie()
    {
        // Retardo inicial
        yield return new WaitForSeconds(spawnDelay * 2);
        
        while (numZombies < zombieMax)
        {
            Instantiate(zombie, transform.position, Quaternion.identity);

            numZombies++;
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
